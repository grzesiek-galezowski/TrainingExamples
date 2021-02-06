using NUnit.Framework;

namespace XUnitTestPatterns._02_Basics
{
  public class Ex01FourPhaseTest
  {
    [Test]
    public void ShouldAllowAccessingItsName()
    {
      //GIVEN
      var anyName = "Zenek";
      var user = new User(anyName, "Ziomal");

      //WHEN
      var name = user.Name;

      //THEN
      Assert.AreEqual(anyName, name);
    }

    //TODO where is the 4th phase?

    [Test]
    public void ShouldAllowAccessingItsName2()
    {
      //we don't use this convention
      //ARRANGE
      var anyName = "Zenek";
      var user = new User(anyName, "Ziomal");

      //ACT
      var name = user.Name;

      //ASSERT
      Assert.AreEqual(anyName, name);

      //ANNIHILATE
      //...
    }
  }

  public class User
  {
    public User(string name, string surname)
    {
      Name = name;
      Surname = surname;
    }

    public string Surname { get; }
    public string Name { get; }
  }
}