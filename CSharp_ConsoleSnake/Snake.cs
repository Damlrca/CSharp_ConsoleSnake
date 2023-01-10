using System;
using System.Collections.Generic;

namespace CSharp_ConsoleSnake
{
    class Snake
    {
        private readonly Queue<Point> snake;

        Snake()
        {
            snake = new Queue<Point>();
            Reset();
        }

        public void Reset()
        {
            snake.Clear();
        }

    }
}