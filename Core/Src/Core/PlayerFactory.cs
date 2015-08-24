using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Core
{
    public static class PlayerFactory
    {
        public static List<Player> GeneratePlayers(MainBoard mainBoard, int playersCount, string[] names)
        {
            var result = new List<Player>();
            for (int i = 0; i < playersCount; i++)
            {
                var player = new Player(i, names[i]);
                var doubloons = Constants.DoubloonsByPlayers[playersCount];
                player.ReceiveDoubloons(mainBoard.TakeDoubloons(doubloons));
                var plantation =
                    new Plantation(Constants.PlantationsByPlayersOrder[new Tuple<int, int>(i + 1, playersCount)]);
                player.Board.BuildPlantation(plantation);

                result.Add(player);
            }

            return result;
        }
    }
}