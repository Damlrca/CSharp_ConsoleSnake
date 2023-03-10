using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ConsoleSaverUtility;

namespace CSharp_ConsoleSnake
{
    class Board
    {
        private readonly PointType[,] map;
        private readonly Snake snake;
        private readonly int FrameDelay;
        private readonly Random random = new Random();
        private int Score;
        private const ConsoleColor textColor = ConsoleColor.Black;
        private const ConsoleColor scoreColor = ConsoleColor.DarkRed;
        private Queue<ConsoleKey> KeyBuffer;
        private ConsoleKey PreviousKey;

        public Board(int width = 31, int height = 21, int frameDelay = 100)
        {
            map = new PointType[width, height];
            snake = new Snake(map);
            FrameDelay = frameDelay;
            Score = 0;
            KeyBuffer = new Queue<ConsoleKey>();
        }

        public void Start()
        {
            Reset();

            StartMessage();

            Refresh_Score();

            Stopwatch stopwatch = new Stopwatch();
            while (true)
            {
                stopwatch.Restart();

                while (stopwatch.ElapsedMilliseconds < FrameDelay / 2 && Console.KeyAvailable)
                {
                    var temp = Console.ReadKey(true).Key;
                    if (temp != PreviousKey)
                    {
                        KeyBuffer.Enqueue(temp);
                        PreviousKey = temp;
                    }
                }

                ConsoleKey key = ConsoleKey.Spacebar;
                //if (Console.KeyAvailable)
                //    key = Console.ReadKey(true).Key;
                if (KeyBuffer.Count > 0)
                    key = KeyBuffer.Dequeue();

                bool move_done = true;
                switch (key)
                {
                    case ConsoleKey.W:
                        key = ConsoleKey.UpArrow;
                        break;
                    case ConsoleKey.D:
                        key = ConsoleKey.RightArrow;
                        break;
                    case ConsoleKey.A:
                        key = ConsoleKey.LeftArrow;
                        break;
                    case ConsoleKey.S:
                        key = ConsoleKey.DownArrow;
                        break;
                    case ConsoleKey.Escape:
                        move_done = false;
                        break;
                    default:
                        break;
                }

                List<Point> ToRedraw = new List<Point>();

                if (snake.Move(key, ToRedraw))
                {
                    if (ToRedraw.Count == 2) // Apple eaten
                    {
                        Score++;
                        Refresh_Score();
                        ToRedraw.Add(Generate_apple());

                        Task.Run(() => Console.Beep(1200, 150));
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

            Task.Run(() => Console.Beep(200, 600));
            GameoverMessage();
        }

        private void StartMessage()
        {
            Console.BackgroundColor = (ConsoleColor)PointType.Border;
            Console.ForegroundColor = textColor;
            Console.SetCursorPosition(2, 0);
            Console.Write("Press any key to start");

            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                ConsoleSaver.Terminate();

            Console.SetCursorPosition(2, 0);
            Console.Write("                      ");
        }

        private void GameoverMessage()
        {
            Console.BackgroundColor = (ConsoleColor)PointType.Border;
            Console.ForegroundColor = textColor;
            Console.SetCursorPosition(2, 0);
            Console.Write("Game over! Final score: ");
            Console.ForegroundColor = scoreColor;
            Console.Write(Score);
            Console.ForegroundColor = textColor;
            Console.Write(". Press any key to continue");

        }

        private void Refresh_Score()
        {
            Console.BackgroundColor = (ConsoleColor)PointType.Border;
            Console.ForegroundColor = textColor;
            Console.SetCursorPosition(2, 0);
            Console.Write("Score: ");
            Console.ForegroundColor = scoreColor;
            Console.Write(Score);
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

            KeyBuffer.Clear();
            PreviousKey = ConsoleKey.RightArrow;

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