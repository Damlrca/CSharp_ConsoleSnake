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
        private readonly Random random = new Random();
        private int Score;

        public Board(int width = 31, int height = 21, int frameDelay = 100)
        {
            map = new PointType[width, height];
            snake = new Snake(map);
            FrameDelay = frameDelay;
            Score = 0;
        }

        public void Start()
        {
            Reset();

            // <press any button>
            Console.BackgroundColor = (ConsoleColor)PointType.Border;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(2, 0);
            Console.WriteLine("Press any key to play");

            Console.ReadKey(true);

            Console.SetCursorPosition(2, 0);
            Console.WriteLine("                     ");
            // </press any button>

            // <score>
            Console.SetCursorPosition(2, 0);
            Console.BackgroundColor = (ConsoleColor)PointType.Border;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"Score: {Score}");
            // </score>

            Stopwatch stopwatch = new Stopwatch();
            while (true)
            {
                stopwatch.Restart();

                ConsoleKey key = ConsoleKey.Spacebar;
                if (Console.KeyAvailable)
                    key = Console.ReadKey(true).Key;

                List<Point> ToRedraw = new List<Point>();

                bool move_done = true;
                if (snake.Move(key, ToRedraw))
                {
                    if (ToRedraw.Count == 2)
                    {
                        Score++;
                        // <score>
                        Console.SetCursorPosition(2, 0);
                        Console.BackgroundColor = (ConsoleColor)PointType.Border;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"Score: {Score}");
                        // </score>
                        ToRedraw.Add(Generate_apple());
                    }
                }
                else
                {
                    move_done = false;
                }

                foreach (var point in ToRedraw)
                {
                    DrawPointOnBoard(point.Left, point.Top);
                }

                if (!move_done)
                    break;

                int t = FrameDelay - (int)stopwatch.ElapsedMilliseconds;
                if (t > 0)
                    Thread.Sleep(t);
            }

            // <gameover>
            Console.BackgroundColor = (ConsoleColor)PointType.Border;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(2, 0);
            Console.WriteLine($"Game over! Final score: {Score}");
            // </gameover>
        }

        private Point Generate_apple()
        {
            int left = random.Next(map.GetLength(0));
            int top = random.Next(map.GetLength(1));
            while (map[left, top] != PointType.Empty)
            {
                left = random.Next(map.GetLength(0));
                top = random.Next(map.GetLength(1));
            }
            map[left, top] = PointType.Apple;
            return new Point(left, top);
        }

        private void Reset()
        {
            Score = 0;

            Console.CursorVisible = false;
            Console.SetWindowSize((map.GetLength(0) + 2) * 2, map.GetLength(1) + 2);

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = PointType.Empty;

            snake.Reset();

            Generate_apple();

            Redraw();
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
                    DrawPointOnBoard(i, j);
        }

        private void DrawPointOnBoard(int left, int top)
        {
            DrawPoint(left + 1, top + 1, map[left, top]);
        }

        private void DrawPoint(int left, int top, PointType type)
        {
            Console.SetCursorPosition(left * 2, top);
            Console.BackgroundColor = (ConsoleColor)type;
            Console.Write("  ");
        }
    }
}