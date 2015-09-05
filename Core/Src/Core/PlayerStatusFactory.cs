using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.IslandObjects;
using Core.Utils;

namespace Core.Core
{
    public static class PlayerStatusFactory
    {
        public static List<PlayerStatus> GeneratePlayers(MainBoardController mainBoardController, int playersCount, string[] names)
        {
            var result = new List<PlayerStatus>();
            for (int i = 0; i < playersCount; i++)
            {
                var player = new PlayerStatus(i, names[i]);
                var doubloons = Constants.DoubloonsByPlayers[playersCount];
                player.ReceiveDoubloons(mainBoardController.TakeDoubloons(doubloons));
                var plantation =
                    new Plantation(Constants.PlantationsByPlayersOrder[new Tuple<int, int>(i + 1, playersCount)]);
                player.Board.BuildPlantation(plantation);

                result.Add(player);
            }

            return result;
        }
    }
}