using System;
using Core.Core;
using Core.PlayerCore;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connector1 = new ManualPlayerConnection("Artur", 0);
            var connector2 = new ManualPlayerConnection("Stepan", 1);
            var connector3 = new ManualPlayerConnection("Petro", 2);

            var game = new GameController(connector1, connector2, connector3);

            game.Start();


            Console.ReadKey();
        }
    }
}