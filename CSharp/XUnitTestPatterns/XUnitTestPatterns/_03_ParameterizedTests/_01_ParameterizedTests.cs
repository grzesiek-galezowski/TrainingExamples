using System.Collections.Generic;
using NUnit.Framework;

namespace XUnitTestPatterns._03_ParameterizedTests
{
  public class _01_ParameterizedTests
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
    public void ShouldBeAdultDependingOnAge2(
      int age, 
      bool expectedIsAdult, 
      object o) //show how this looks like in both resharper and NCrunch and VSTest
    {
      //GIVEN
      var person = new Person(age);

      //WHEN
      var isAdult = person.IsAdult();

      //THEN
      Assert.AreEqual(expectedIsAdult, isAdult);
    }

    [Test, TestCaseSource(nameof(DataWithSameStringRepresentation))]
    public void ProblemWithSomeTools(List<int> list) //show how this looks like in both resharper and NCrunch and VSTest
    {
      CollectionAssert.IsNotEmpty(list);
    }

    // Also show when several parameter lists have the same string representation
    // - in such a case, the tests may not be detected correctly
    private static object[] ShouldBeAdultDependingOnAgeData() => new object[]
    {
      new object[] {17, false, new object()},
      new object[] {18, true, new object()},
      new object[] {18, true, new object()},
    };
    
    // NCrunch will not pick it up
    private static object[] DataWithSameStringRepresentation() => new object[]
    {
      new object[] { new List<int> {1,2,3}},
      new object[] { new List<int> {1}},
      new object[] { new List<int> {5,6,7,8}},
    };
  }
}