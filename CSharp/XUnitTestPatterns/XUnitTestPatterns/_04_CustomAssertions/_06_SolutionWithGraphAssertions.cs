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

    record PersonData(string Name, string Surname, int Age)
    {
      public static PersonData ClarkKent()
      {
        return new PersonData("Clark", "Kent", 35);
      }

      public static PersonData Superman()
      {
        return new PersonData("Clark", "Kent", 35); //break this assertion
      }
    }
  }
}
