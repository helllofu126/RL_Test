import os
import gymnasium as gym
import tenacity
from google.protobuf import empty_pb2
from gymnasium import spaces
from asyncio import timeout
import numpy as np
import random
from tenacity import stop_never
import grpc

from Service import EnvServicer


# 定义一个环境类，继承gym.Env
class CustomEnv(gym.Env):
    """
    自定义环境
    """
    # 元数据
    metadata = {
        "render_modes": ["human", "rgb_array"],
        "render_fps": 30
    }

    def __init__(
            self,
            servicer: EnvServicer):
        # 调用父类的构造函数
        super(CustomEnv, self).__init__()
        self.servicer = servicer  # gRPC 双向流
        # 定义动作空间和观测空间
        self.action_space = spaces.Discrete(5)
        self.observation_space = spaces.Box(low=0, high=100,shape=(4,), dtype=int)

    def reset(self, seed=None, options=None):
        """
        重置环境
        :param seed:
        :param options:
        :return:
        """

        # 发送重置请求
        result =self.send_rest()
        return result

    # 定义重试策略
    @tenacity.retry(wait=tenacity.wait_fixed(5), stop=stop_never)
    def send_rest(self):
        """
        发送重置请求
        :return:
        """
        try:
            observation = self.servicer.send_reset_and_wait_response()
            return observation , {}
        except Exception as e:
            # 打印异常信息
            print(f"发送重置请求失败:{e}")
            # 重新抛出异常以触发重试
            raise

    def step(self, action):
        """
        执行动作
        :param action: 动作
        :return:返回状态，奖励，是否结束，是否超时截断，信息
        """

        # 如果action类型不是数组,则转为数组
        if not isinstance(action, list):
            action = [action]

        # 发送动作请求
        result = self.send_step_action(action)
        return result


    # 定义重试策略
    @tenacity.retry(wait=tenacity.wait_fixed(5), stop=stop_never)
    def send_step_action(self, action):
        """
        发送动作请求
        :param action:
        :return:
        """
        try:
            # 发送动作请求
            return self.servicer.send_step_and_wait_response(action)
        except Exception as e:
            # 打印异常信息
            print(f"发送动作请求失败:{e}")
            # 重新抛出异常以触发重试
            raise
