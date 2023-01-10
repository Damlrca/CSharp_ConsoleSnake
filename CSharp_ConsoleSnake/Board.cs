using System;
using System.Diagnostics;
using System.Threading;

namespace CSharp_ConsoleSnake
{
    class Board
    {
        private readonly PointType[,] Map;
        //private readonly Snake snake;
        public Board(int width = 21, int height = 31)
        {
            Map = new PointType[width + 2, height + 2];
            
            for (int i = 0; i < Map.GetLength(0); i++)
                ChangePointState(i, 0, PointType.Border);

            for (int i = 0; i < Map.GetLength(0); i++)
                ChangePointState(i, Map.GetLength(1) - 1, PointType.Border);

            for (int i = 0; i < Map.GetLength(1); i++)
                ChangePointState(0, i, PointType.Border);

            for (int i = 0; i < Map.GetLength(1); i++)
                ChangePointState(Map.GetLength(0) - 1, i, PointType.Border);

            Reset();
        }

        public void Reset()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(Map.GetLength(0) * 2, Map.GetLength(1));

            for (int i = 1; i + 1 < Map.GetLength(0); i++)
                for (int j = 1; j + 1 < Map.GetLength(1); j++)
                    ChangePointState(i, j, PointType.Empty);

            Redraw();
        }

        public void Start()
        {
            // Write Start!
            // while (true) ...
            // Game over: result
        }

        private void Redraw()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
                for (int j = 0; j < Map.GetLength(1); j++)
                    DrawPoint(i, j, Map[i, j]);
        }

        private void DrawPoint(int left, int top, PointType type)
        {
            Console.SetCursorPosition(2 * left, top);
            Console.BackgroundColor = (ConsoleColor)type;
            Console.Write("  ");
        }

        private void ChangePointState(int left, int top, PointType type)
        {
            Map[left, top] = type;
        }
    }
}