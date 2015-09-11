using System.Linq;
using Core.Core;
using Core.Entities;
using Core.Entities.Buildings;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class PlayerControllerTests
    {
        [Test]
        public void DoSelectBuildingForBuild_True()
        {
            var status = new PlayerStatus(1, "Test");
            status.ReceiveDoubloons(10);
            var mainBoardController = new MainBoardController(4);
            var playerController = new PlayerController(mainBoardController, status);

            var university = new University();
            university.ReceiveColonist();

            status.Board.BuildBuilding(university);

            var result = playerController.DoSelectBuildingToBuild(mainBoardController.Status.Buildings.First().Key, true);

            Assert.IsTrue(result);
        }
    }
}
