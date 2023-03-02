using UnitTestRunnerPackageExercise;

namespace ExampleTests;

public class Class1
{
  public void Test1()
  {
    Assert.AreEqual(1, 1);
  }

  public void Test2()
  {
    Assert.Fail("lol");
  }
}