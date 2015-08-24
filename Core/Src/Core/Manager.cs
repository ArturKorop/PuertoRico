using System;
using Core.Entities;

namespace Core.Core
{
    public class Manager
    {
        private readonly PlayerController _controller;

        public Manager(PlayerController playerController)
        {
            _controller = playerController;
        }

        public PlayerBoard Board
        {
            get { return _controller.Player.Board; }
        }

        public bool IsGameEnd
        {
            get
            {
                return !Board.CanBuildIslandObject() || !Board.CanBuildBuilding(1);
            }
        }

        public Roles PlayFirstRoundTurn()
        {
            Console.WriteLine("Select Roles: Builder: 'b'; Captain: 'c'");
            var key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case 'c':
                    return Roles.Captain;
                case 'b':
                    return Roles.Builder;
                default:
                    return Roles.Prospector;
            }
        }

        public void PlayRoundTurn(Roles role)
        {
            Console.WriteLine("Play role: {0}", role);
        }
    }
}