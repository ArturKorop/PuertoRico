using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Utils;

namespace Core.Core
{
    public class GameController
    {
        private readonly MainBoard _mainBoard;

        private readonly List<Player> _players;

        public GameController(params IPlayerConnection[] connections)
        {
            var playerCount = connections.Length;
            _mainBoard = new MainBoard(playerCount);
            var names = Util.Shuffle(connections.Select(x => x.Name)).ToArray();
            _players = PlayerFactory.GeneratePlayers(_mainBoard, playerCount, names);
            for (int i = 0; i < playerCount; i++)
            {
                connections[i].Init(new PlayerController(_players[i], _mainBoard));
            }
        }
    }

    public static class PlayerFactory
    {
        public static List<Player> GeneratePlayers(MainBoard mainBoard, int count, string[] names)
        {
            var result = new List<Player>();
            for (int i = 0; i < count; i++)
            {
                var player = new Player(i, names[i]);
                var doubloons = Constants.DoubloonsByPlayers[count];
                player.ReceiveDoubloons(mainBoard.TakeDoubloons(doubloons));
            }
        }
    }

    public interface IPlayerConnection
    {
        void Init(PlayerController playerController);
        string Name { get; }
    }
}