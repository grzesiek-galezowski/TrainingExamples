using NUnit.Framework;
using XUnitTestPatterns._03_ParameterizedTests;

namespace XUnitTestPatterns._06_PickingTestValues
{
  public class _02_BoundaryValuesAndConstantTests
  {
    [TestCase(Person.AdultAge, true)]
    [TestCase(Person.AdultAge - 1, false)]
    public void ShouldBeAdultDependingOnAge(int age, bool expectedIsAdult)
    {
      //GIVEN
      var person = new Person(age);

      //WHEN
      var isAdult = person.IsAdult();

      //THEN
      Assert.AreEqual(expectedIsAdult, isAdult);
    }

    [Test]
    public void ShouldDefineAdultAgeAs18()
    {
      Assert.AreEqual(18, Person.AdultAge);
    }

  }
}