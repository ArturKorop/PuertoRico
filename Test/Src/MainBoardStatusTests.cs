using Core.Entities;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class MainBoardStatusTests
    {
        [Test]
        public void Clone_Successfully()
        {
            var status = new MainBoardStatus(4);

            var clone = status.Clone();
            clone.Vp = 10;
            clone.Doubloons = 15;

            Assert.AreNotEqual(status.Vp, clone.Vp);
            Assert.AreNotEqual(status.Doubloons, clone.Vp);
            Assert.AreEqual(status.Colonists, clone.Colonists);
        }
    }
}
