using NUnit.Framework;

namespace XUnitTestPatterns._02_Basics
{
  public class _01_FourPhaseTest
  {
    [Test]
    public void ShouldAllowAccessingItsName()
    {
      //GIVEN
      var initialName = "Zenek";
      var user = new User(initialName, "Ziomal");

      //WHEN
      var retrievedName = user.Name;

      //THEN
      Assert.AreEqual(initialName, retrievedName);
    }

    //TODO where is the 4th phase?

    [Test]
    public void ShouldAllowAccessingItsName2()
    {
      //we don't use this convention
      //ARRANGE
      var initialName = "Zenek";
      var user = new User(initialName, "Ziomal");

      //ACT
      var retrievedName = user.Name;

      //ASSERT
      Assert.AreEqual(initialName, retrievedName);

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