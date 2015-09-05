using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Base;
using Core.Entities.Buildings;
using Core.Entities.RoleParameters;
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
            var result = _market.SimulateSellGoods(Goods.Corn, _buildings);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestBuildingAction_LargeMarketWithColonist()
        {
            _largetMarket.ReceiveColonist();
            var result = _market.SimulateSellGoods(Goods.Corn, _buildings);

            Assert.AreEqual(2, result);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestBuildingAction_LargeMarketTooMuchColonists()
        {
            _largetMarket.ReceiveColonist();
            var result = _market.SimulateSellGoods(Goods.Corn, _buildings);

            _largetMarket.ReceiveColonist();
        }
    }
}
