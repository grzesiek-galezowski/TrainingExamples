using NUnit.Framework;

namespace unit_tests_csharp.P01CoreRunner
{
  //Questions:
  //1. Which one runs first?
  //2. Do both pass?
  public class Ex01RememberingDataBetweenTests
  {
    private int _i = 0;

    [Test]
    public void ShouldIncrementANumberOneTime()
    {
      _i++;
      //Assert.AreEqual(1, _i); //TODO incomment
    }

    [Test]
    public void ShouldAlsoIncrementANumberOneTime()
    {
      _i++;
      //Assert.AreEqual(1, _i); //TODO incomment
    }
  }
}
