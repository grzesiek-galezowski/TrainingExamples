using System;
using NUnit.Framework;

namespace XUnitTestPatterns._04_CustomAssertions
{
  public class _01_NoTrueFalseAssertions
  {

    [Test]
    public void UsingAssertTrue()
    {
      //show assertion errors
      Assert.True(1 == 4);
    }

    [Test]
    public void UsingSpecificAssertion()
    {
      //show assertion errors
      Assert.AreEqual(4, 1);
    }

    [Test]
    public void ShouldCreateSupermanWithIdenticalDataAsClarkKent()
    {
      //GIVEN
      var clark = PersonData.ClarkKent();

      //WHEN
      var superman = PersonData.Superman();

      //THEN
      Assert.True(AreTheSame(clark, superman));
    }

    private bool AreTheSame(PersonData clark, PersonData superman)
    {
      return clark.Age == superman.Age
             && clark.Name.Equals(superman.Name)
             && clark.Surname.Equals(superman.Surname);
    }

    class PersonData
    {
      public readonly string Name;
      public readonly string Surname;
      public readonly int Age;

      PersonData(string name, string surname, int age)
      {
        this.Name = name;
        this.Surname = surname;
        this.Age = age;
      }

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
