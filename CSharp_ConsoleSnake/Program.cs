using System;
using System.Threading;

namespace CSharp_ConsoleSnake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            while (true)
            {
                board.Start();
                Thread.Sleep(1000);
                Console.ReadKey(true);
            }
        }
    }
}
