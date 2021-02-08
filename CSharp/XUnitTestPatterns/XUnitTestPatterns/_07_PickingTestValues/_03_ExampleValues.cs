using NUnit.Framework;

namespace XUnitTestPatterns._07_PickingTestValues
{
  public class _03_ExampleValues
  {
    [TestCase("aaaaa", false, "must start with capital letter")]
    [TestCase("A", false, "cannot be a one-letter name")]
    [TestCase("Aa", true, "valid input")]
    [TestCase("0", false, "cannot have digits")]
    [TestCase("&", false, "cannot have special chars")]
    public void ShouldCorrectlyCheckNames(string nameString, bool expectedResult, string comment)
    {
      //GIVEN
      var userNameCheck = new UserNameCheck();

      //WHEN
      var isUserNameValid = userNameCheck.PassesFor(nameString);

      //THEN
      Assert.AreEqual(expectedResult, isUserNameValid);
    }
  }
}