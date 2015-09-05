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
            var result = _market.SimulateSellGoods(Goods.Corn, _buildings);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void TestBuildingAction_OfficeWithColonist()
        {
            _office.ReceiveColonist();
            var result = _market.SimulateSellGoods(Goods.Corn, _buildings);

            Assert.AreEqual(0, result);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestBuildingAction_OfficeTooMuchColonists()
        {
            _office.ReceiveColonist();
            var result = _market.SimulateSellGoods(Goods.Corn, _buildings);

            _office.ReceiveColonist();
        }
    }
}
