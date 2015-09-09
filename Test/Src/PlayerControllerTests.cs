using System.Linq;
using Core.Core;
using Core.Entities;
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

            var result = playerController.DoSelectBuildingToBuild(mainBoardController.Status.Buildings.First().Key);

            Assert.IsTrue(result);
        }
    }
}
