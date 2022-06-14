using System;
using NUnit.Framework;

namespace XUnitTestPatterns._01_SetupTeardownDataLife
{
  public class _01_SetupsAndTeardowns
  {
    /*
     * OneTimeSetUp
     *
     * SetUp
     * Test1
     * TearDown
     *
     * SetUp
     * Test2
     * TearDown
     *
     * OneTimeTearDown
     */


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      Console.WriteLine(nameof(OneTimeSetup));
    }

    [SetUp]
    public void Setup()
    {
      Console.WriteLine(nameof(Setup));
    }

    [Test]
    public void Test1()
    {
      Console.WriteLine(nameof(Test1));
    }

    [Test]
    public void Test2()
    {
      Console.WriteLine(nameof(Test2));
    }

    [TearDown]
    public void TearDown()
    {
      Console.WriteLine(nameof(TearDown));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
      Console.WriteLine(nameof(OneTimeTearDown));
    }
  }
}