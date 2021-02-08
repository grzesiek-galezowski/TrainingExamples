using System;
using NUnit.Framework;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace XUnitTestPatterns._07_FixtureManagement
{
  public class _05_TestDataBuilders //2 examples
  {
    [Test]
    public void ShouldIncludeUserNameInAppendedUserLog1()
    {
      //GIVEN
      var user = new UserBuilder1().WithName("Zenek").Build();
      var inMemoryLog = new InMemoryLog();

      //WHEN
      inMemoryLog.LogUser(user);

      //THEN
      StringAssert.Contains("Zenek", inMemoryLog.RetrieveEntries()[^1]);
    }

    class UserBuilder1
    {
      private string _name = Any.String();
      private string _surname = Any.String();
      private int _age = Any.Integer();

      public UserBuilder1 WithName(string name)
      {
        _name = name;
        return this;
      }

      public UserBuilder1 WithSurname(string surname)
      {
        _surname = surname;
        return this;
      }

      public UserBuilder1 WithAge(int age)
      {
        _age = age;
        return this;
      }

      public User Build()
      {
        return new User(_name, _surname, _age);
      }
    }
    
    [Test]
    public void ShouldIncludeUserNameInAppendedUserLog2()
    {
      //GIVEN
      var user = new UserBuilder2()
      {
        Name = "Zenek"
      }.Build();
      var inMemoryLog = new InMemoryLog();
      inMemoryLog.LogUser(user);

      //WHEN
      var entries = inMemoryLog.RetrieveEntries();

      //THEN
      StringAssert.Contains("Zenek", entries[^1]);
    }

    class UserBuilder2
    {
      public string Name { get; init; } = Any.String();
      public string Surname { get; init; } = Any.String();
      public int Age { get; init; } = Any.Integer();

      public User Build()
      {
        return new User(Name, Surname, Age);
      }
    }

  }
}