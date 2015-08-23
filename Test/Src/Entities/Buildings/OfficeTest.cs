using System.Collections.Generic;
using Core.ActionsData;
using Core.Entitites;
using Core.Entitites.Buildings;
using NUnit.Framework;

namespace Test.Entities.Buildings
{
    [TestFixture]
    public class OfficeTest
    {
        private Market _market;

        private Office _office;

        private IEnumerable<BuildingBase<TraderParameters>> _buildings;

        [SetUp]
        public void Initialize()
        {
            _market = new Market();
            _office = new Office();
            _buildings = new BuildingBase<TraderParameters>[] { _office };
            _market.SellGood(Goods.Corn, _buildings);
        }

        [Test]
        public void TestBuildingAction_OfficeWithoutColonist()
        {
            var result = _market.SimulateSellGood(Goods.Corn, _buildings);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void TestBuildingAction_OfficeWithColonist()
        {
            _office.AddColonist();
            var result = _market.SimulateSellGood(Goods.Corn, _buildings);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestBuildingAction_OfficeTooMuchColonists()
        {
            _office.AddColonist();
            var result = _market.SimulateSellGood(Goods.Corn, _buildings);

            Assert.AreEqual(false, _office.AddColonist());
            Assert.AreEqual(0, result);
        }
    }
}
