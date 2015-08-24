using System;
using Core.Core;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connector1 = new ManualPlayerConnection("Artur");
            var connector2 = new ManualPlayerConnection("Stepan");
            var connector3 = new ManualPlayerConnection("Petro");

            var game = new GameController(connector1, connector2, connector3);

            game.Start();

            Console.WriteLine(connector1.Status());

            Console.ReadKey();
        }
    }
}