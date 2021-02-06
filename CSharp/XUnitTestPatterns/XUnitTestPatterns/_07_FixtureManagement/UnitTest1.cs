using NUnit.Framework;

namespace XUnitTestPatterns._07_FixtureManagement
{

  public class Tests7
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