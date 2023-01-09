using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace CSharp_ConsoleSnake
{
    internal class Program
    {
        static int score;
        static private int FrameDelay = 300;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            PointType[,] m = new PointType[20, 30];

            Console.SetWindowSize(m.GetLength(1) * 2 + 4, m.GetLength(0) + 2);

            for (int i = 0; i < m.GetLength(1) + 2; i++)
                DrawPoint(i, 0, PointType.Border);

            for (int i = 0; i < m.GetLength(1) + 2; i++)
                DrawPoint(i, m.GetLength(0) + 1, PointType.Border);

            for (int i = 1; i < m.GetLength(0) + 1; i++)
                DrawPoint(0, i, PointType.Border);

            for (int i = 1; i < m.GetLength(0) + 1; i++)
                DrawPoint(m.GetLength(1) + 1, i, PointType.Border);

            Queue<Point> snake = new Queue<Point>();

            Point head = new Point(0, 0);

            for (int i = 1; i <= 10; i++)
            {
                m[1, i] = PointType.Snake;
                Point temp = new Point(1, i);
                snake.Enqueue(temp);
                head = temp;
            }

            m[head.X, head.Y] = PointType.Head;

            ConsoleKey pressedKey = ConsoleKey.RightArrow;

            Stopwatch sw = new Stopwatch();

            Redraw(m);

            while (true)
            {
                sw.Restart();

                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (pressedKey != ConsoleKey.RightArrow)
                                pressedKey = ConsoleKey.LeftArrow;
                            break;
                        case ConsoleKey.RightArrow:
                            if (pressedKey != ConsoleKey.LeftArrow)
                                pressedKey = ConsoleKey.RightArrow;
                            break;
                        case ConsoleKey.UpArrow:
                            if (pressedKey != ConsoleKey.DownArrow)
                                pressedKey = ConsoleKey.UpArrow;
                            break;
                        case ConsoleKey.DownArrow:
                            if (pressedKey != ConsoleKey.UpArrow)
                                pressedKey = ConsoleKey.DownArrow;
                            break;
                        default:
                            break;
                    }
                }

                m[head.X, head.Y] = PointType.Snake;
                DrawPoint(head.Y + 1, head.X + 1, m[head.X, head.Y]); 

                switch (pressedKey)
                {
                    case ConsoleKey.LeftArrow:
                        head.Y = (head.Y + m.GetLength(1) - 1) % m.GetLength(1);
                        break;
                    case ConsoleKey.RightArrow:
                        head.Y = (head.Y + 1) % m.GetLength(1);
                        break;
                    case ConsoleKey.UpArrow:
                        head.X = (head.X + m.GetLength(0) - 1) % m.GetLength(0);
                        break;
                    case ConsoleKey.DownArrow:
                        head.X = (head.X + 1) % m.GetLength(0);
                        break;
                    default:
                        break;
                }

                m[head.X, head.Y] = PointType.Head;
                DrawPoint(head.Y + 1, head.X + 1, m[head.X, head.Y]);
                
                Point back = snake.Dequeue();
                m[back.X, back.Y] = PointType.Empty;
                DrawPoint(back.Y + 1, back.X + 1, m[back.X, back.Y]);
                
                snake.Enqueue(head);

                int t = 200 - (int)sw.ElapsedMilliseconds;
                if (t > 0)
                    Thread.Sleep(t);
            }
        }

        static void Restart()
        {

        }

        static void Redraw(PointType[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(1); j++)
                    DrawPoint(j + 1, i + 1, m[i, j]);
        }

        static void DrawPoint(int left, int top, PointType p)
        {
            Console.SetCursorPosition(2 * left, top);
            switch (p)
            {
                case PointType.Empty:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case PointType.Apple:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case PointType.Snake:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case PointType.Head:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case PointType.Border:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                default:
                    break;
            }
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
        }
    }

    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    enum PointType
    {
        Empty = 0,
        Apple,
        Snake,
        Head,
        Border
    }
}
