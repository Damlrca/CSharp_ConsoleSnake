using System;

namespace CSharp_ConsoleSnake
{
    struct Point
    {
        public int Left { get; set; }
        public int Top { get; set; }

        public Point(int left, int top)
        {
            Left = left;
            Top = top;
        }
    }

    enum PointType
    {
        Empty = ConsoleColor.Black,
        Apple = ConsoleColor.Red,
        Snake = ConsoleColor.Green,
        Head = ConsoleColor.DarkGreen,
        Border = ConsoleColor.Gray,
        GameoverHead = ConsoleColor.Blue
    }
}