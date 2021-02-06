using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace XUnitTestPatterns._01_SetupTeardownDataLife
{
  public class FixtureLifecycle
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
