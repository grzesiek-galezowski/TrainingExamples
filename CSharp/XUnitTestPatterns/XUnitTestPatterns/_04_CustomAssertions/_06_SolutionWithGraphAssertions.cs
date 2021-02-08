using System;
using FluentAssertions;
using NUnit.Framework;

namespace XUnitTestPatterns._04_CustomAssertions
{
  public class _06_SolutionWithGraphAssertions
  {
    [Test]
    public void ShouldCreateSupermanWithIdenticalDataAsClarkKent()
    {
      //GIVEN
      var clark = PersonData.ClarkKent();

      //WHEN
      var superman = PersonData.Superman();

      //THEN
      superman.Should().BeEquivalentTo(clark);
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
        return new PersonData("Clark", "Kent", 351);
      }
    }
  }
}
