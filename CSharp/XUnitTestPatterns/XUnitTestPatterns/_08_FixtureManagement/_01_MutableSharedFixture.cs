using NUnit.Framework;

namespace XUnitTestPatterns._08_FixtureManagement
{
  public class _01_MutableSharedFixture
  {
    private int _number = 0;

    [Test]
    public void Test1()
    {
      _number++;
      Assert.AreEqual(1, _number);
    }

    [Test]
    public void Test2()
    {
      _number++;
      Assert.AreEqual(1, _number);
    }
  }

  //bonus - how to enable instance per test case
  [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
  public class FixtureLifecycle2
  {
    private int _number = 0;

    [Test]
    public void Test1()
    {
      _number++;
      Assert.AreEqual(1, _number);
    }

    [Test]
    public void Test2()
    {
      _number++;
      Assert.AreEqual(1, _number);
    }
  }
  //bug paralellizable?
}
