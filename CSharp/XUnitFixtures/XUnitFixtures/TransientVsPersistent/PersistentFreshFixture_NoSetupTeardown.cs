using NUnit.Framework;

namespace XUnitFixtures.TransientVsPersistent
{
    public class PersistentFreshFixture_NoSetupTeardown //naive version
    {
        private int _x = 1;
        private int _y = 2;

        [Test]
        public void Test1()
        {
            Assert.AreEqual(1, _x);
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(2, _y);
        }
    }

    //TODO less naive version
}