using NUnit.Framework;

namespace NUnitParallelizableAttribute
{
    // class-level parallelizable (with different scopes? What about setup? SingleInstancePerTest or sth.), inheritance
    // test-level parallelizable (part of the tests, all tests)
    // assembly level (All, Children etc.)
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}