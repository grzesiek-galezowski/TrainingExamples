using NUnit.Framework;

namespace unit_tests_csharp.P01CoreRunner
{
  public class Ex03ParameterizedTests
  {
    [TestCase(2, 2, "What did you expect?")] //change the values
    [TestCase(3, 3, "Surprised?")]
    public void ShouldDoSomething(int a, int b, string message)
    {
      Assert.AreEqual(b,a,message);
    }

    /////////////////////////

    static readonly object[] DataForShouldUseExternalValues =
    {
      //change the values
      new object[] { 3, 3, "What did you expect?" },
      new object[] { 3, 3, "Surprised?" },
    };

    [Test, TestCaseSource(nameof(DataForShouldUseExternalValues))]
    public void ShouldUseExternalValues(int a, int b, string message)
    {
      Assert.AreEqual(b, a, message);
    }


  }
}
