using NUnit.Framework;

namespace XUnitFixtures.TransientVsPersistent
{
    public class PersistentFreshFixture_NoSetupTeardown //naive version
    {
        private int _x;
        private int _y;

        [Test]
        public void Test1()
        {
            _x = 1;
            Assert.AreEqual(1, _x);
        }

        [Test]
        public void Test2()
        {
            _y = 2;
            Assert.AreEqual(2, _y);
        }
    }

    //TODO less naive version
}