using System;
using NUnit.Framework;

namespace XUnitTestPatterns._04_CustomAssertions
{
  public class _03_SolutionWithPurelyCustomAssertions
  {

    [Test]
    public void ShouldCreateSupermanWithIdenticalDataAsClarkKent()
    {
      //GIVEN
      var clark = PersonData.ClarkKent();

      //WHEN
      var superman = PersonData.Superman();

      //THEN
      AssertThatAreTheSamePerson(superman, clark);
    }

    private void AssertThatAreTheSamePerson(PersonData superman, PersonData clark)
    {
      Assert.AreEqual(clark.Name, superman.Name);
      Assert.AreEqual(clark.Surname, superman.Surname);
      Assert.AreEqual(clark.Age, superman.Age);
    }

    record PersonData(string Name, string Surname, int Age)
    {
      public static PersonData ClarkKent()
      {
        return new PersonData("Clark", "Kent", 35);
      }

      public static PersonData Superman()
      {
        return new PersonData("Clark", "Kent", 35);
      }
    }

  }
}
