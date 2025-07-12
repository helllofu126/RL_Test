import os
import queue
import threading
import time

import grpc
from concurrent import futures
import gymnasium as  gym
import numpy as np
from pyparsing import Empty
from torch.backends.mkl import verbose

import gym_env_pb2 as pb
import gym_env_pb2_grpc
from  gym_env_pb2 import *
from google.protobuf import empty_pb2
from stable_baselines3 import PPO as SB3_PPO, DQN as SB3_DQN, SAC as SB3_SAC
from stable_baselines3 import TD3 as SB3_TD3, A2C as SB3_A2C,DDPG as SB3_DDPG

class EnvServicer(gym_env_pb2_grpc.EnvServicer):
    def __init__(self):
        # 发送给客户端的消息队列
        self._send_queue = queue.Queue()
        # 接收客户端消息的迭代器（在EnvInteraction中赋值）
        self._client_iterator = None
        # 用来同步阻塞等待响应
        self._response_queue = queue.Queue()
        self.env_id = "CustomEnv_V1"  # 默认环境ID
        # 创建一个字典，将模型名称映射到相应的加载函数
        self.model_loaders = {
            "PPO": SB3_PPO,
            "DQN": SB3_DQN,
            "DDPG": SB3_DDPG,
            "SAC": SB3_SAC,
            "TD3": SB3_TD3,
            "A2C": SB3_A2C
        }
        # 模型目录路径
        self.model_dir_path = "models"
        #初始化模型字典
        self.model= None
        self.model_ids = self.load_model()



    def load_model(self):
        """
        加载模型
        """
        # 判断文件夹下是否有model文件夹
        if not os.path.exists(self.model_dir_path):
            os.makedirs(self.model_dir_path)
            return
        #获取模型文件夹下的所有文件
        model_files=os.listdir(self.model_dir_path)
        if not model_files or len(model_files) == 0:
            print("[服务端] 没有找到模型文件")
            return []
        #获取模型ID
        model_ids=[model_file.split('.')[0] for model_file in model_files]

        #加载初始模型
        model_path= os.path.join(self.model_dir_path, model_files[0])
        print(f"[服务端] 加载初始模型: {model_path}")
        # 加载模型
        env = gym.make(self.env_id, servicer=self)
        self.model=self.model_loaders[model_ids[0]].load(model_path,env=env)
        return model_ids

    def EnvInteraction(self, request_iterator, context):
        # print("[服务端] 客户端连接建立")
        print(f"[服务端] 新连接，request_iterator id={id(request_iterator)}")
        self._client_iterator = request_iterator

        try:
            while True:
                # 从发送队列拿消息，发给客户端
                msg = self._send_queue.get()
                if msg is None:
                    print("[服务端] 关闭 EnvInteraction 流")
                    break  # 收到 None 表示要关闭流，退出循环结束发送
                yield msg  # 把消息发送给客户端（yield 代表这是一个生成器，grpc框架会使用它发送消息）
        except Exception as e:
            print(f"[服务端] EnvInteraction异常: {e}")

    def _send_msg(self, msg):
        #打印发送的消息类型
        # print(f"[服务端] 发送消息类型:")
        self._send_queue.put(msg)

    def _wait_for_response(self, expected_field, timeout=15):
        # 从客户端消息迭代器等待响应消息
        start = time.time()
        while True:
            if time.time() - start > timeout:
                raise TimeoutError(f"[服务端] 等待{expected_field}响应超时")

            if self._client_iterator is None:
                raise RuntimeError("[服务端] 尚未建立客户端连接")

            try:
                # 获取下一个消息
                # print("[服务端] 等待客户端消息...")
                msg = next(self._client_iterator)
            except StopIteration:
                raise RuntimeError("[服务端] 客户端连接断开")

            if msg.HasField(expected_field):
                return getattr(msg, expected_field)

            # 没收到想要的消息，继续循环等待

    def send_reset_and_wait_response(self):
        # print("[服务端] 发送 ResetRequest")
        reset_req = pb.ResetRequest(env_id="env-001")
        msg = pb.EnvMessage(reset_request=reset_req)
        self._send_msg(msg)

        response = self._wait_for_response("reset_response")
        observation = np.array(response.observation)
        # print("[服务端] 收到 ResetResponse:", observation)
        return observation

    def send_step_and_wait_response(self, action):
        # print(f"[服务端] 发送 StepRequest 动作: {action}")
        step_request = pb.Step(action=action)
        msg = pb.EnvMessage(step_request=step_request)
        self._send_msg(msg)

        response = self._wait_for_response("step_response")
        observation = np.array(response.observation)
        # print(f"[服务端] 收到 StepResponse 观测:{observation} 奖励:{response.reward}")
        return observation, response.reward, response.terminated, response.truncated, dict(response.info)

    def send_training_end_and_wait_response(self):
        """
        发送训练结束请求，并等待响应
        """
        print("[服务端] 发送 TrainingEnd 请求")
        training_end = pb.TrainingEnd(is_end=True)
        msg = pb.EnvMessage(training_end_request=training_end)
        self._send_msg(msg)

        response = self._wait_for_response("training_end_response")
        self._send_queue.put(None)  # 发送 None 关闭 EnvInteraction 流
        print("[服务端] 收到 TrainingEndResponse")
        return response

    def TrainingEnv(self, request, context):
        """
        请求训练模型
        """

        #获取请求的环境ID
        env_id = request.env_id
        #获取请求的算法
        algorithm = AlgorithmType.Name(request.algorithm)
        #获取请求的策略
        policy = request.policy
        #获取请求的折扣因子
        gamma = request.gamma
        #获取请求的学习率
        learning_rate = request.learningRate
        #获取请求的训练步数
        training_steps = request.totalTimeSteps
        #获取设置的设备
        device = request.device

        # 这里可以添加训练环境的逻辑
        print(f"[服务端] 收到 TrainingEnv 请求: env_id={env_id}, algorithm={algorithm}, policy={policy}, "
              f"gamma={gamma}, learning_rate={learning_rate}, training_steps={training_steps}, device={device}")

        #开始训练模型，模型以algorithm命名
        # 获取模型路径
        model_path = os.path.join(self.model_dir_path, algorithm)
        env = gym.make(env_id, servicer=self)
        #如果模型存在则加载模型
        if algorithm in self.model_ids:
            print(f"[服务端] 加载已存在的模型: {model_path}")
            model = self.model_loaders[algorithm].load(model_path,env=env,gamma=gamma,learning_rate=learning_rate, device=device,verbose=2)
        else:
            print(f"[服务端] 创建新的模型: {model_path}")
            model = self.model_loaders[algorithm](policy=policy, env=env, gamma=gamma, learning_rate=learning_rate, device=device,verbose=2)

        # 创建线程，在线程中训练模型，因为训练模型可能会阻塞
        thread = threading.Thread(target=self._model_learn,
                                  args=(algorithm, policy, model, model_path, training_steps))
        # 启动线程
        thread.start()

        # 返回空响应
        return empty_pb2.Empty()

    def _model_learn(self, algorithm, policy, model, model_path, total_time_steps):
        """
        模型训练逻辑
        """
        print(f"[服务端] 开始训练模型: {model_path}，算法: {algorithm}, 策略: {policy}")
        model.learn(total_timesteps=total_time_steps)
        print(f"[服务端] 模型训练完成，保存到: {model_path}")
        model.save(model_path)
        self.model= model
        #通知客户端模型训练完成
        self.send_training_end_and_wait_response()


    def RequestStepAction(self, request, context):
        """
        请求动作
        """

        #获取观测空间值
        observation = np.array(request.observation)
        #使用模型预测动作
        if self.model is None:
            print("[服务端] 模型未加载，无法预测动作")
            return pb.Step(action=[0])
        action, _ = self.model.predict(observation, deterministic=True)
        print(f"[服务端] 预测动作: {action}，观测: {observation}")
        # 如果动作为0维
        if action.ndim == 0:
            action = [action]
        # 返回动作
        return pb.Step(action=action)


def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    gym_env_pb2_grpc.add_EnvServicer_to_server(EnvServicer(), server)
    port=8081
    server.add_insecure_port(f'[::]:{port}')
    server.start()
    print(f"[服务端] gRPC 服务启动，监听端口{port}")
    server.wait_for_termination()
    #通过一个无限循环使程序持续运行。time.sleep(3600) 使线程每隔一小时休眠一次，这样可以减少 CPU 占用
    try:
        while True:
            time.sleep(3600)
    except KeyboardInterrupt:
        server.stop(0)
if __name__ == '__main__':
    serve()
