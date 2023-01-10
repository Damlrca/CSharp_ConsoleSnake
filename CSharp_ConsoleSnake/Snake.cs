﻿using System;
using System.Collections.Generic;

namespace CSharp_ConsoleSnake
{
    class Snake
    {
        private readonly Queue<Point> snake;
        public Point Head;
        private readonly PointType[,] Map;
        private ConsoleKey pressedKey;

        public Snake(PointType[,] map)
        {
            snake = new Queue<Point>();
            Map = map;
            Reset();
        }

        public void Reset()
        {
            snake.Clear();
            for (int left = 1; left <= 4; left++)
            {
                Map[left, 1] = PointType.Snake;
                snake.Enqueue(new Point(left, 1));
                Head = new Point(left, 1);
            }
            Map[Head.Left, Head.Top] = PointType.Head;
            pressedKey = ConsoleKey.RightArrow;
        }

        public bool Move(ConsoleKey key, List<Point> ToRedraw) {
            switch (key)
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
            Map[Head.Left, Head.Top] = PointType.Snake;
            ToRedraw.Add(Head);
            switch (pressedKey)
            {
                case ConsoleKey.LeftArrow:
                    Head.Left = (Head.Left + Map.GetLength(0) - 1) % Map.GetLength(0);
                    break;
                case ConsoleKey.RightArrow:
                    Head.Left = (Head.Left + 1) % Map.GetLength(0);
                    break;
                case ConsoleKey.UpArrow:
                    Head.Top = (Head.Top + Map.GetLength(1) - 1) % Map.GetLength(1);
                    break;
                case ConsoleKey.DownArrow:
                    Head.Top = (Head.Top + 1) % Map.GetLength(1);
                    break;
                default:
                    break;
            }
            ToRedraw.Add(Head);
            snake.Enqueue(Head);
            Map[Head.Left, Head.Top] = PointType.Head;
            Point Back = snake.Dequeue();
            Map[Back.Left, Back.Top] = PointType.Empty;
            ToRedraw.Add(Back);
            return true;
        }
    }
}