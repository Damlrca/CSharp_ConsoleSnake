using System;
using System.Threading;
using ConsoleSaverUtility;

namespace CSharp_ConsoleSnake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleSaver.Save();

            Console.Title = "Snake";
            Board board = new Board();
            while (true)
            {
                board.Start();
                Thread.Sleep(2000);
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    ConsoleSaver.Terminate();
            }
        }
    }
}
