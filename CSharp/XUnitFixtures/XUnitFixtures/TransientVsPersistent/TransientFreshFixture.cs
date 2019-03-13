using NUnit.Framework;

namespace XUnitFixtures.TransientVsPersistent
{
    public class TransientFreshFixture
    {
        [Test]
        public void Test1()
        {
            int x = 1;
            Assert.AreEqual(1, x);
        }
    }
}