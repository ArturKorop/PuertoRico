using System;
using Core.Core;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connector1 = new PlayerConnection("Artur");
            var connector2 = new PlayerConnection("Stepan");
            var connector3 = new PlayerConnection("Petro");

            var game = new GameController(connector1, connector2, connector3);

            game.Start();

            Console.WriteLine(connector1.Status());

            Console.ReadKey();
        }
    }
}