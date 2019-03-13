using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace XUnitFixtures
{
    public class PersistentSharedFixture
    {
        //if this happened in the setup, this would be fresh
        private readonly List<int> _emptyList = new List<int>();

        [Test]
        public void Test1()
        {
            Assert.False(_emptyList.Any());
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(0, _emptyList.Count());
        }

        [Test]
        public void Test3()
        {
            Assert.AreEqual(0, _emptyList.Count);
        }
    }
}