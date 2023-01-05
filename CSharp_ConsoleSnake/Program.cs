using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_ConsoleSnake
{
    internal class Program
    {
        static int score;
        static void Main(string[] args)
        {
            PointType[,] m = new PointType[10, 20];

            for (int i = 0; i < m.GetLength(0) + 2; i++)
                DrawPoint(i, 0, PointType.Border);

            for (int j = 1; j < m.GetLength(1) + 1; j++)
                DrawPoint(0, 2 * j, PointType.Border);

            for (int i = 0; i < m.GetLength(0) + 2; i++)
                DrawPoint(i, 2 * (m.GetLength(1) + 1), PointType.Border);

            for (int j = 1; j < m.GetLength(1) + 1; j++)
                DrawPoint(m.GetLength(0) + 1, 2 * j, PointType.Border);

            Queue<Point> snake = new Queue<Point>();

            Point head = new Point(0, 0);

            for (int i = 1; i <= 5; i++)
            {
                m[1, i] = PointType.Snake;
                Point temp = new Point(1, i);
                snake.Enqueue(temp);
                head = temp;
            }

            m[head.X, head.Y] = PointType.Head;

            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo(' ', ConsoleKey.RightArrow, false, false, false);

            Task.Run(() =>
            {
                while (true)
                {
                    ConsoleKeyInfo temp = Console.ReadKey();
                    switch (temp.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (pressedKey.Key != ConsoleKey.RightArrow)
                                pressedKey = temp;
                            break;
                        case ConsoleKey.RightArrow:
                            if (pressedKey.Key != ConsoleKey.LeftArrow)
                                pressedKey = temp;
                            break;
                        case ConsoleKey.UpArrow:
                            if (pressedKey.Key != ConsoleKey.DownArrow)
                                pressedKey = temp;
                            break;
                        case ConsoleKey.DownArrow:
                            if (pressedKey.Key != ConsoleKey.UpArrow)
                                pressedKey = temp;
                            break;
                        default:
                            break;
                    }   
                }
                    
            });

            while (true)
            {
                m[head.X, head.Y] = PointType.Snake;
                switch (pressedKey.Key)
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
                snake.Enqueue(head);
                Point back = snake.Dequeue();
                m[back.X, back.Y] = PointType.Empty;

                Redraw(m);

                Thread.Sleep(300);
            }
        }

        static void Restart()
        {

        }

        static void Redraw(PointType[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(1); j++)
                    DrawPoint(i + 1, 2 * j + 2, m[i, j]);
        }

        static void DrawPoint(int x, int y, PointType p)
        {
            Console.SetCursorPosition(y, x);
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
            Console.SetCursorPosition(0, 12);
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
