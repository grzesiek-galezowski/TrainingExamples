using NUnit.Framework;

namespace XUnitFixtures
{
    public class PersistentFreshFixture_SetupTeardown
    {
        private int _x;

        [SetUp]
        public void Initialize()
        {
            _x = 1;
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(1, _x);
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(2, _x + 1);
        }
    }
}