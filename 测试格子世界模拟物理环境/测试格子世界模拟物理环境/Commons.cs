using System;

namespace 测试格子世界模拟物理环境
{
    /// <summary>
    /// 枚举：方向
    /// </summary>
    public enum EnumDirection
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 0,

        /// <summary>
        /// 上
        /// </summary>
        Down = 1,

        /// <summary>
        /// 下
        /// </summary>
        Right = 2,

        /// <summary>
        /// 左
        /// </summary>
        Up = 3,

        /// <summary>
        /// 右
        /// </summary>
        Left = 4
    }

    /// <summary>
    /// 机器人类
    /// </summary>
    public class Robot
    {
        /// <summary>
        /// 起点坐标
        /// </summary>
        public Point StartPoint { get; set; }

        /// <summary>
        /// 目标坐标
        /// </summary>
        public Point TargetPoint { get; set; }

        //构造函数
        public Robot(Point start)
        {
            StartPoint = new Point(start.X, start.Y);
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="point"></param>
        public void Reset(Point point)
        {
            StartPoint = new Point(point.X, point.Y);
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="action"></param>
        public void Move(EnumDirection selectDirection)
        {
            switch (selectDirection)
            {
            case EnumDirection.Stop:
                break;
            case EnumDirection.Up:
                StartPoint.Y--;
                break;
            case EnumDirection.Down:
                StartPoint.Y++;
                break;
            case EnumDirection.Left:
                StartPoint.X--;
                break;
            case EnumDirection.Right:
                StartPoint.X++;
                break;
            }
        }

    }

    /// <summary>
    /// 用于表示一个坐标
    /// </summary>
    public class Point
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 计算两个点之间的距离：曼哈顿距离
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int Distance(Point p)
        {
            return Math.Abs(X - p.X) + Math.Abs(Y - p.Y);
        }

        /// <summary>
        /// 判断两点是否相同
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsEquals(Point point)
        {
            return X == point.X && Y == point.Y;
        }
    }
}
