namespace CSharp_ConsoleSnake
{
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
        Apple = 12,
        Snake = 10,
        Head = 2,
        Border = 7
    }
}