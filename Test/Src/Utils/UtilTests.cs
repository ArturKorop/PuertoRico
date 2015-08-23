using System.Collections.Generic;
using System.Linq;
using Core.Entitites;
using Core.Utils;
using NUnit.Framework;

namespace Test.Utils
{
    [TestFixture]
    public class UtilTests
    {
        [Test]
        public void ShuffleTest_NormalData_Successfull()
        {
            IEnumerable<Plantation>[] sourcePlantations =
            {
                MainFactory.GetPlantions(5, Goods.Corn),
                MainFactory.GetPlantions(5, Goods.Tabacco),
                MainFactory.GetPlantions(5, Goods.Sugar),
                MainFactory.GetPlantions(5, Goods.Coffee)
            };

            var plantations = Util.Shuffle(sourcePlantations).ToList();

            Assert.AreEqual(20, plantations.Count());
            Assert.IsTrue(plantations.Take(5).Select(x => x.Type).Distinct().Count() > 1);
        }
    }
}