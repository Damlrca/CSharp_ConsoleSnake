using System;
using System.Threading;

namespace CSharp_ConsoleSnake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Console.ReadKey(true);
            board.Start();
        }
    }
}
