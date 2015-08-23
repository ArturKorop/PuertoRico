using System.Collections.Generic;
using Core.ActionsData;
using Core.Entitites;
using Core.Entitites.Buildings;
using NUnit.Framework;

namespace Test.Entities.Buildings
{
    [TestFixture]
    public class LargetMarketTest
    {
        private Market _market;

        private LargeMarket _largetMarket;

        private IEnumerable<BuildingBase<TraderParameters>> _buildings;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _market = new Market();
            _largetMarket = new LargeMarket();
            _buildings = new BuildingBase<TraderParameters>[] { _largetMarket };
        }

        [Test]
        public void TestBuildingAction_LargeMarketWithoutColonist()
        {
            var result = _market.SimulateSellGood(Goods.Corn, _buildings);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestBuildingAction_LargeMarketWithColonist()
        {
            _largetMarket.AddColonist();
            var result = _market.SimulateSellGood(Goods.Corn, _buildings);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void TestBuildingAction_LargeMarketTooMuchColonists()
        {
            _largetMarket.AddColonist();
            var result = _market.SimulateSellGood(Goods.Corn, _buildings);

            Assert.AreEqual(false, _largetMarket.AddColonist());
            Assert.AreEqual(2, result);
        }
    }
}
