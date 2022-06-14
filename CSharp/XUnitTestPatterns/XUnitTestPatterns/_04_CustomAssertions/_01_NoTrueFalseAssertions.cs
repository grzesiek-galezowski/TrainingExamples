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

    [Test]
    public void ShouldCreateSupermanWithIdenticalDataAsClarkKent__MultipleMode()
    {
      //GIVEN
      var clark = PersonData.ClarkKent();

      //WHEN
      var superman = PersonData.Superman();

      //THEN
      Assert.Multiple(() =>
      {
        Assert.AreEqual(clark.Age, superman.Age);
        Assert.AreEqual(clark.Name, superman.Name);
        Assert.AreEqual(clark.Surname, superman.Surname);
      });
    }
    
    [Test]
    public void ShouldCreateSupermanWithIdenticalDataAsClarkKent__EqualityObject()
    {
      //GIVEN
      var clark = PersonData.ClarkKent();

      //WHEN
      var superman = PersonData.Superman();

      //THEN
      Assert.AreEqual(new EquatablePerson(clark), new EquatablePerson(superman));
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
        Name = name;
        Surname = surname;
        Age = age;
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
    
    private class EquatablePerson
    {
      public readonly PersonData Actual;

      public EquatablePerson(PersonData actual)
      {
        Actual = actual;
      }

      override public bool Equals(object obj)
      {
        var other = (EquatablePerson) obj;
        return Actual.Surname == other.Actual.Surname
               && Actual.Age == other.Actual.Age
               && Actual.Name == other.Actual.Name;
      }

      override public string ToString()
      {
        return $"{Actual.Name} {Actual.Surname}, age {Actual.Age}";
      }
    }
  }

}
