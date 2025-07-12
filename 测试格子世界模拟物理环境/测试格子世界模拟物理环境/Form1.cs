using Grpc.Core;
using GymEnv;
using SkiaSharp;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GymEnv.Env;

namespace 测试格子世界模拟物理环境
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 通道
        /// </summary>
        private Channel _channel;

        /// <summary>
        /// 客户端
        /// </summary>
        public EnvClient Client { get; private set; }

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// 实现线程安全的发送队列
        /// </summary>
        BlockingCollection<EnvMessage> _responseQueue = null;

        /// <summary>
        /// 双向通道流对象
        /// </summary>
        AsyncDuplexStreamingCall<EnvMessage, EnvMessage> _envStream;

        /// <summary>
        /// 接收线程
        /// </summary>
        Thread _receiveTread;

        /// <summary>
        /// 发送线程
        /// </summary>
        Thread _sendTread;

        private async void buttonConnect_Click(object sender, System.EventArgs e)
        {
            // 检查是否已经连接
            if (IsConnected && _channel.State == ChannelState.Ready)
            {
                MessageBox.Show("已经连接到服务器！");
                return;
            }

            //获取IP地址和端口号
            string ipAddress = textBoxIP.Text;
            string port = textBoxPort.Text;
            //连接到服务器
            if (string.IsNullOrEmpty(ipAddress) || string.IsNullOrEmpty(port))
            {
                MessageBox.Show("IP地址或端口号不能为空！");
                return;
            }
            try
            {
                int portNumber = int.Parse(port);

                //创建grpc通道
                _channel = new Channel(ipAddress, portNumber, ChannelCredentials.Insecure);

                //创建客户端
                Client = new EnvClient(_channel);

                //连接服务端
                await _channel.ConnectAsync().ContinueWith(task =>
                 {
                     if (task.Status == TaskStatus.RanToCompletion)
                     {
                         //连接成功
                         IsConnected = true;
                     }
                 });

            }
            catch (FormatException)
            {
                MessageBox.Show("端口号格式不正确，请输入一个有效的数字。");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败: {ex.Message}");
            }

        }

        /// <summary>
        /// 发送信息线程
        /// </summary>
        private async void Thread_Send()
        {
            try
            {
                // 发送线程，持续从队列中取出消息并发送到服务端
                foreach (var envMessage in _responseQueue.GetConsumingEnumerable())
                {
                    // 将EnvMessage对象发送到服务端
                    _envStream.RequestStream.WriteAsync(envMessage).Wait();
                }

                // 4. 正式关闭写入流，结束双向流（不等待服务端回应）
                await _envStream.RequestStream.CompleteAsync();
            }
            catch (Exception ex)
            {
                //弹出错误信息
                MessageBox.Show($"发送线程异常: {ex.Message}");
            }
        }

        /// <summary>
        ///接收信息线程
        /// </summary>
        private async void Thread_Receive()
        {
            // 接收线程，持续接收服务端发送的消息
            try
            {
                for (; ; )
                {
                    // 等待服务端发送的消息
                    bool isReceive = await _envStream.ResponseStream.MoveNext();
                    if (!isReceive)
                    {
                        // 如果没有消息，等待下次接收
                        Thread.Sleep(5);
                        continue;
                    }

                    //如果设置了刷新间隔，则等待指定的时间
                    if (checkBoxIsRefresh.Checked && Controls.ContainsKey(textBoxRefreshInterval))
                    {
                        var textBox = Controls[textBoxRefreshInterval] as TextBox;
                        if (int.TryParse(textBox.Text, out int refreshInterval) && refreshInterval > 0)
                        {
                            Thread.Sleep(refreshInterval);
                        }
                    }

                    // 获取服务端发送的消息
                    EnvMessage serviceMessage = _envStream.ResponseStream.Current;

                    //如果接收到重置环境的信息
                    if (serviceMessage.ResetRequest != null)
                    {
                        // 重置环境
                        ResetEnvironment();
                    }
                    else if (serviceMessage.StepRequest != null)
                    {
                        //获取动作信息,并执行动作
                        int action = serviceMessage.StepRequest.Action[0];
                        StepAction(action);

                    }
                    else if (serviceMessage.TrainingEndRequest != null)
                    {
                        //重置环境
                        ResetEnvironment();

                        //刷新画布
                        skglControlGridWorld.Invalidate();


                        //训练结束，结束信息侦听
                        // 1. 回复服务端 TrainingEndResponse
                        var response = new EnvMessage
                        {
                            TrainingEndResponse = new TrainingEnd
                            {
                                IsEnd = true,
                            }
                        };
                        _responseQueue.Add(response);

                        // 2. 停止发送队列
                        _responseQueue.CompleteAdding();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"接收线程异常: {ex.Message}");
            }
        }

        private int _stepCount = 0;

        private int _lastAction = -1;

        /// <summary>
        /// 接收到重置环境的响应后，重置环境
        /// </summary>
        /// <returns></returns>
        private void ResetEnvironment()
        {
            //重置环境
            ResetEnv();

            _stepCount = 0;

            _lastAction = -1;

            //获取机器人坐标
            var robotPoint = _robot.StartPoint;

            //获取目标点坐标
            var targetPoint = _robot.TargetPoint;

            //刷新画布
            if (checkBoxIsRefresh.Checked)
            {
                skglControlGridWorld.Invalidate();
            }

            //返回重置响应
            EnvMessage envMessage = new EnvMessage();
            var resetResponse = new ResetResponse { Observation = { robotPoint.X, robotPoint.Y, targetPoint.X, targetPoint.Y } };
            envMessage.ResetResponse = resetResponse;
            _responseQueue.Add(envMessage);
            //_envStream.RequestStream.WriteAsync(envMessage);
        }

        /// <summary>
        /// 动作奖励计算
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private double ActionReward(int action)
        {
            // 如果上次动作为空，则初始化并返回0
            if (_lastAction == -1)
            {
                _lastAction = action;
                return 0;
            }

            // 如果不动，给予惩罚
            if (action == 0)
            {
                return -0.5;
            }

            // 判断动作是否相同，并返回相应的奖励
            double reward = action == _lastAction ? 0.1 : -0.1;

            // 更新上次动作
            _lastAction = action;

            return reward;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private void StepAction(int action)
        {
            //初始化奖励
            float reward = 0;
            float robotReward = 0;
            bool success = false;
            bool failure = false;
            bool truncated = false;

            //获取机器人坐标
            var oldRobotPoint = new Point(_robot.StartPoint.X, _robot.StartPoint.Y);

            //获取目标点坐标
            var targetPoint = new Point(_robot.TargetPoint.X, _robot.TargetPoint.Y);

            //将动作转换为枚举
            EnumDirection actionEnum = (EnumDirection)action;

            //移动机器人
            _robot.Move(actionEnum);

            //新的位置
            var newRobotPoint = _robot.StartPoint;

            double actionReward = ActionReward(action);

            //如果当前机器人位置超出边界，则奖励为-5
            if (newRobotPoint.X < 0 || targetPoint.X >= GRID_WIDTH || newRobotPoint.Y < 0 || targetPoint.Y >= GRID_HEIGHT)
            {
                _robot.StartPoint = new Point(oldRobotPoint.X, oldRobotPoint.Y);

                reward = -5;
                failure = true;
            }
            else if (newRobotPoint.IsEquals(targetPoint))
            {
                reward = 100;
                success = true;
            }
            else
            {
                //分别计算新位置和之前的位置离目标点的曼哈顿距离
                var oldDistance = oldRobotPoint.Distance(targetPoint);
                var newDistance = newRobotPoint.Distance(targetPoint);

                var distance = oldDistance - newDistance;

                //如果新位置离目标点更近，则奖励为1
                if (distance > 0)
                {
                    robotReward = 1;
                }
                else if (distance < 0)
                {
                    robotReward = -1;
                }
                else
                {
                    robotReward = (float)-0.5;
                }

                reward = (float)(robotReward + actionReward - 0.1);

                _stepCount++;

                //如果步数超过最大步数，则失败
                if (_stepCount >= GRID_HEIGHT * GRID_WIDTH)
                {
                    truncated = true;
                    reward = -100;
                }
            }

            //刷新画布
            if (checkBoxIsRefresh.Checked)
            {
                skglControlGridWorld.Invalidate();
            }

            //如果
            bool terminated = success || failure;

            //返回结果
            StepResult stepResult = new StepResult
            {
                Reward = reward,
                Observation = { newRobotPoint.X, newRobotPoint.Y, targetPoint.X, targetPoint.Y },
                Info = { },
                Truncated = truncated,
                Terminated = terminated
            };

            // 创建EnvMessage对象
            EnvMessage envMessage = new EnvMessage
            {
                StepResponse = stepResult,
            };
            // 将EnvMessage对象发送到服务端
            //_envStream.RequestStream.WriteAsync(envMessage);
            _responseQueue.Add(envMessage);
        }

        //机器人
        Robot _robot;

        //格子分辨率
        private const int GRID_RESOLUTION = 25;

        //格子宽度和高度
        private const int GRID_WIDTH = 20;
        private const int GRID_HEIGHT = 20;

        /// <summary>
        /// 重置环境
        /// </summary>
        private void ResetEnv()
        {
            //随机生成机器人起点
            Random random = new Random();
            int robotX = random.Next(0, GRID_WIDTH);
            int robotY = random.Next(0, GRID_HEIGHT);

            //初始化robot
            _robot = new Robot(new Point(robotY, robotY));

            //随机生成目标点
            int targetX = random.Next(0, GRID_WIDTH);
            int targetY = random.Next(0, GRID_HEIGHT);

            //如果目标点和机器人起点重合，则重新生成
            while (robotX == targetX && robotY == targetY)
            {
                targetX = random.Next(0, GRID_WIDTH);
                targetY = random.Next(0, GRID_HEIGHT);
            }

            //设置目标点
            _robot.TargetPoint = new Point(targetX, targetY);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //开启定时器，刷新连接情况
            timerRefresh.Enabled = true;
            timerRefresh.Tick += TimerRefresh_Tick;

            //重置环境
            ResetEnv();

            //将AlgorithmType以大写字符串的形式显示在comboBox中
            comboBoxAlgorithmType.DataSource = Enum.GetValues(typeof(AlgorithmType));
            comboBoxAlgorithmType.SelectedIndex = 1; // 默认选择第一个算法类型
        }

        const string labelName = "labelRefreshInterval";
        const string textBoxRefreshInterval = "textBoxRefreshInterval";

        private void TimerRefresh_Tick(object sender, EventArgs e)
        {
            //检查连接状态
            if (IsConnected && _channel.State == ChannelState.Ready)
            {
                labelStatus.Text = "在线";
                labelStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                labelStatus.Text = "离线";
                labelStatus.ForeColor = System.Drawing.Color.Red;
            }

            //如果复选框被选中，则添加一个label,内容是刷新间隔世界，label位置在复选框下方，原本的按钮要向下移动
            if (checkBoxIsRefresh.Checked)
            {
                //检查是否已经添加了label
                if (Controls[labelName] == null)
                {
                    //添加label
                    Label label = new Label
                    {
                        Name = labelName,
                        Text = "刷新间隔（毫秒）：",
                        Location = new System.Drawing.Point(label9.Location.X, checkBoxIsRefresh.Location.Y + checkBoxIsRefresh.Height + 5),
                        AutoSize = true
                    };
                    Controls.Add(label);
                    //添加文本框
                    TextBox textBox = new TextBox
                    {
                        Name = textBoxRefreshInterval,
                        Location = new System.Drawing.Point(label.Location.X + label.Width + 5, label.Location.Y - 3),
                        Width = 50,
                        Text = 0.ToString(),
                    };
                    Controls.Add(textBox);

                    //将模型训练按钮和动作预测按钮向下移动
                    button1.Location = new System.Drawing.Point(button1.Location.X, label.Location.Y + label.Height + 10);
                    //button2.Location = new System.Drawing.Point(button2.Location.X, button1.Location.Y + button1.Height + 10);

                }
            }
            else
            {
                //如果复选框没有被选中，则移除label和文本框
                if (Controls[labelName] != null)
                {
                    Controls.RemoveByKey(labelName);
                }
                if (Controls[textBoxRefreshInterval] != null)
                {
                    Controls.RemoveByKey(textBoxRefreshInterval);

                    //将模型训练按钮和动作预测按钮向上移动
                    button1.Location = new System.Drawing.Point(button1.Location.X, checkBoxIsRefresh.Location.Y + checkBoxIsRefresh.Height + 10);
                }
                //button2.Location = new System.Drawing.Point(button2.Location.X, button1.Location.Y + button1.Height + 10);
            }

        }

        private void skglControlGridWorld_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            //获取画布
            var canvas = e.Surface.Canvas;

            //底色为白色
            Color winColor = SystemColors.Control;             // 获取 WinForms 控件默认颜色
            SKColor skiColor = new SKColor(winColor.R, winColor.G, winColor.B);  // 转换为 SKColor
            canvas.Clear(skiColor);

            //绘制格子
            for (int i = 0; i < GRID_WIDTH; i++)
            {
                for (int j = 0; j < GRID_HEIGHT; j++)
                {
                    //绘制格子
                    DrawRect(canvas, i, j, SKColors.Black, SKPaintStyle.Stroke);
                }
            }

            //获取机器人坐标
            var robotPoint = _robot.StartPoint;

            //绘制机器人
            DrawRobot(canvas, robotPoint.X, robotPoint.Y);

            //获取目标点坐标
            var targetPoint = _robot.TargetPoint;

            //绘制目标点
            DrawRect(canvas, targetPoint.X, targetPoint.Y, SKColors.BlueViolet);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        private void DrawRect(SKCanvas canvas, int x, int y, SKColor color, SKPaintStyle sKPaintStyle = SKPaintStyle.Fill)
        {
            //计算矩形左上角坐标
            var left = x * GRID_RESOLUTION;
            var top = y * GRID_RESOLUTION;

            //绘制格子
            canvas.DrawRect(left, top, GRID_RESOLUTION, GRID_RESOLUTION, new SKPaint()
            {
                Color = color,
                Style = sKPaintStyle,
            });
        }

        /// <summary>
        /// 绘制机器人
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawRobot(SKCanvas canvas, int x, int y)
        {
            //计算圆心坐标
            var centerX = x * GRID_RESOLUTION + GRID_RESOLUTION / 2;
            var centerY = y * GRID_RESOLUTION + GRID_RESOLUTION / 2;


            canvas.DrawCircle(centerX + 1, centerY + 1, (GRID_RESOLUTION / 2), new SKPaint()
            {
                Color = SKColors.SeaGreen,
                Style = SKPaintStyle.Fill
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //检查是否连接
            if (!IsConnected || _channel.State != ChannelState.Ready)
            {
                MessageBox.Show("请先连接到服务器！");
                return;
            }

            // 停止旧线程
            _receiveTread?.Abort();
            _sendTread?.Abort();

            // 3. 启动双向流
            _responseQueue = new BlockingCollection<EnvMessage>();

            // 发送线程（将响应从队列中取出发给服务端）
            _envStream = Client.EnvInteraction();

            // 启动接收线程
            _receiveTread = new Thread(Thread_Receive);
            _receiveTread.IsBackground = true;
            _receiveTread.Name = "ReceiveThread";
            _receiveTread.Start();

            // 启动发送线程
            _sendTread = new Thread(Thread_Send);
            _sendTread.IsBackground = true;
            _sendTread.Name = "SendThread";
            _sendTread.Start();


            //获取环境id
            string envId = textBoxEnvId.Text;

            //获取算法类型
            if (!Enum.TryParse(comboBoxAlgorithmType.SelectedItem.ToString(), out AlgorithmType algorithmType))
            {
                MessageBox.Show("请选择正确的算法类型！");
                return;
            }

            //获取策略
            string policy = textBoxPolicy.Text;

            //获取学习率
            if (!float.TryParse(textBoxLeaningRate.Text, out float learningRate))
            {
                MessageBox.Show("学习率格式不正确，请输入一个有效的数字。");
                return;
            }

            //获取折扣率
            if (!float.TryParse(textBoxGamma.Text, out float gamma))
            {
                MessageBox.Show("折扣率格式不正确，请输入一个有效的数字。");
                return;
            }

            //获取epsilon
            if (!int.TryParse(textBoxTotalTimeSteps.Text, out int epsilon))
            {
                MessageBox.Show("Epsilon格式不正确，请输入一个有效的数字。");
                return;
            }

            //获取设备
            string device = textBoxDevice.Text;

            //创建请求类
            var request = new TrainingEnvRequest
            {
                EnvId = envId,
                Algorithm = algorithmType,
                Policy = policy,
                LearningRate = learningRate,
                Gamma = gamma,
                TotalTimeSteps = epsilon,
                Device = device
            };

            //发送请求到服务器
            try
            {
                var response = Client.TrainingEnv(request);
            }
            catch (RpcException rpcEx)
            {
                MessageBox.Show($"RPC错误: {rpcEx.Status.Detail}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}");
            }
        }

        volatile bool _isRequestActionFromModel = true;
        Thread _requestActionThread;

        private void button2_Click(object sender, EventArgs e)
        {
            //检查是否连接
            if (!IsConnected || _channel.State != ChannelState.Ready)
            {
                MessageBox.Show("请先连接到服务器！");
                return;
            }

            button2.Enabled = false;

            //如果请求动作的线程已经存在，则不管
            if (button2.Text.Equals("向模型请求动作"))
            {
                _isRequestActionFromModel = true;

                //创建请求动作的线程
                _requestActionThread = new Thread(Thread_RequestActionFromModel);
                _requestActionThread.IsBackground = true;
                _requestActionThread.Name = "RequestActionFromModelThread";
                _requestActionThread.Start();

                button2.Text = "停止请求动作";
            }
            else
            {
                button2.Text = "向模型请求动作";
                //停止请求动作的线程
                _isRequestActionFromModel = false;
            }

            button2.Enabled = true;

        }

        private void Thread_RequestActionFromModel()
        {
            try
            {
                //可以请求动作
                for (; ; )
                {
                    //如果不可以请求动作
                    if (!_isRequestActionFromModel)
                    {
                        return;
                    }

                    //请求动作并执行
                    ExecuteRequestActionFromModel();

                    //获取间隔时间
                    if (int.TryParse(textBoxRequestTime.Text, out int requestTime) && requestTime > 0)
                    {
                        //等待指定的时间
                        Thread.Sleep(requestTime);
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// 向模型请求动作并执行
        /// </summary>
        private void ExecuteRequestActionFromModel()
        {
            //封装请求数据
            var request = new ModelPredictActionRequest()
            {
                Observation = { _robot.StartPoint.X, _robot.StartPoint.Y, _robot.TargetPoint.X, _robot.TargetPoint.Y },
            };

            //Debug.WriteLine($"请求数据：{request.Observation[0]},{request.Observation[1]},{request.Observation[2]},{request.Observation[3]}");

            //请求动作
            var response = Client.RequestStepAction(request);

            //获取动作
            var action = response.Action[0];

            //将动作转换为枚举
            EnumDirection actionEnum = (EnumDirection)action;

            //移动机器人
            _robot.Move(actionEnum);

            //刷新画布
            skglControlGridWorld.Invalidate();

            //打印状态
            //Debug.WriteLine($"机器人坐标：{_robot.StartPoint.X},{_robot.StartPoint.Y}");
            //Debug.WriteLine($"目标坐标：{_robot.TargetPoint.X},{_robot.TargetPoint.Y}");


            //等待1秒
            Thread.Sleep(100);

            //如果机器人到达目标点，则重置环境
            if (_robot.StartPoint.X == _robot.TargetPoint.X && _robot.StartPoint.Y == _robot.TargetPoint.Y)
            {
                ResetEnv();
            }
        }
    }
}
