using NUnit.Framework;

namespace XUnitTestPatterns._04_CustomAssertions
{
  public class _05_SolutionWithEqualityObject
  {
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