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

        }
    }
}
