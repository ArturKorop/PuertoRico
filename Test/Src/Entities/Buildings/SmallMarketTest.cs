using System.Collections.Generic;
using Core.ActionsData;
using Core.Entities;
using Core.Entities.Buildings;
using NUnit.Framework;

namespace Test.Entities.Buildings
{
    [TestFixture]
    public class SmallMarketTest
    {
        private Market _market;

        private SmallMarket _smallMarket;

        private IEnumerable<BuildingBase<TraderParameters>> _buildings;

        [SetUp]
        public void Initialize()
        {
            _market = new Market();
            _smallMarket = new SmallMarket();
            _buildings = new BuildingBase<TraderParameters>[] { _smallMarket };
        }

        [Test]
        public void TestBuildingAction_SmallMarketWithoutColonist()
        {
            var result = _market.SimulateSellGoods(Goods.Corn, _buildings);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestBuildingAction_SmallMarketWithColonist()
        {
            _smallMarket.AddColonist();
            var result = _market.SimulateSellGoods(Goods.Corn, _buildings);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void TestBuildingAction_SmallMarketTooMuchColonists()
        {
            _smallMarket.AddColonist();
            var result = _market.SimulateSellGoods(Goods.Corn, _buildings);

            Assert.AreEqual(false, _smallMarket.AddColonist());
            Assert.AreEqual(1, result);
        }
    }
}
