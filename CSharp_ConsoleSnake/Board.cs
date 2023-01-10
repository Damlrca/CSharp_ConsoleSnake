using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CSharp_ConsoleSnake
{
    class Board
    {
        private readonly PointType[,] map;
        private readonly Snake snake;
        private readonly int FrameDelay;
        public Board(int width = 31, int height = 21, int frameDelay = 100)
        {
            map = new PointType[width, height];
            snake = new Snake(map);
            FrameDelay = frameDelay;
            Reset();
        }

        public void Reset()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize((map.GetLength(0) + 2) * 2, map.GetLength(1) + 2);

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    ChangePointState(i, j, PointType.Empty);

            snake.Reset();

            Redraw();
        }

        public void Start()
        {
            // TODO ReadKey to start

            Stopwatch stopwatch = new Stopwatch();
            while (true)
            {
                stopwatch.Restart();

                ConsoleKey key = ConsoleKey.Spacebar;
                if (Console.KeyAvailable)
                    key = Console.ReadKey(true).Key;

                List<Point> ToRedraw = new List<Point>();

                if (!snake.Move(key, ToRedraw))
                {
                    //break
                }

                foreach (var point in ToRedraw)
                {
                    DrawPointOnBoard(point.Left, point.Top, map[point.Left, point.Top]);
                }

                int t = FrameDelay - (int)stopwatch.ElapsedMilliseconds;
                if (t > 0)
                    Thread.Sleep(t);
            }

            // TODO Game over
        }

        private void Redraw()
        {
            for (int i = 0; i < map.GetLength(0) + 2; i++)
                DrawPoint(i, 0, PointType.Border);

            for (int i = 0; i < map.GetLength(0) + 2; i++)
                DrawPoint(i, map.GetLength(1) + 1, PointType.Border);

            for (int i = 1; i < map.GetLength(1) + 1; i++)
                DrawPoint(0, i, PointType.Border);

            for (int i = 1; i < map.GetLength(1) + 1; i++)
                DrawPoint(map.GetLength(0) + 1, i, PointType.Border);

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    DrawPointOnBoard(i, j, map[i, j]);
        }

        private void DrawPointOnBoard(int left, int top, PointType type)
        {
            DrawPoint(left + 1, top + 1, type);
        }

        private void DrawPoint(int left, int top, PointType type)
        {
            Console.SetCursorPosition(left * 2, top);
            Console.BackgroundColor = (ConsoleColor)type;
            Console.Write("  ");
        }

        private void ChangePointState(int left, int top, PointType type)
        {
            map[left, top] = type;
        }
    }
}