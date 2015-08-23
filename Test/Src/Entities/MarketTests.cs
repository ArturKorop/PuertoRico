using System.Collections.Generic;
using Core.ActionsData;
using Core.Entitites;
using NUnit.Framework;

namespace Test.Entities
{
    [TestFixture]
    public class MarketTests
    {
        Market _market;
        IEnumerable<BuildingBase<TraderParameters>> _buildings;

        [SetUp]
        public void Initialize()
        {
            _market = new Market();
            _buildings = new BuildingBase<TraderParameters>[] {};
        }

        [Test]
        public void TestMarket_CanSell()
        {
            Assert.AreEqual(true, _market.CanSellGood(Goods.Corn, false));
        }

        [Test]
        public void TestMarket_SellCorn()
        {
            Assert.AreEqual(0, _market.SellGood(Goods.Corn, _buildings));
            Assert.AreEqual(3, _market.FreeSpaces);
        }

        [Test]
        public void TestMarket_SellCornAgain()
        {
            _market.SellGood(Goods.Corn, _buildings);
            Assert.AreEqual(null, _market.SellGood(Goods.Corn, _buildings));
            Assert.AreEqual(3, _market.FreeSpaces);

            Assert.AreEqual(null, _market.SellGood(Goods.Corn, _buildings));
            Assert.AreEqual(3, _market.FreeSpaces);
        }
    }
}
