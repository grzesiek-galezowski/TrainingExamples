using NUnit.Framework;

namespace XUnitTestPatterns._03_ParameterizedTests
{
  public class Ex04ParameterizedTestPersonSpecification
  {
    [TestCase(17, false)]
    [TestCase(18, true)]
    public void ShouldBeAdultDependingOnAge1(int age, bool expectedIsAdult)
    {
      //GIVEN
      var person = new Person(age);

      //WHEN
      var isAdult = person.IsAdult();

      //THEN
      Assert.AreEqual(expectedIsAdult, isAdult);
    }

    [Test, TestCaseSource(nameof(ShouldBeAdultDependingOnAgeData))]
    public void ShouldBeAdultDependingOnAge2(int age, bool expectedIsAdult)
    {
      //GIVEN
      var person = new Person(age);

      //WHEN
      var isAdult = person.IsAdult();

      //THEN
      Assert.AreEqual(expectedIsAdult, isAdult);
    }

    private static object[] ShouldBeAdultDependingOnAgeData() => new object[]
    {
      new object[] {17, false},
      new object[] {18, true},
    };
  }
}