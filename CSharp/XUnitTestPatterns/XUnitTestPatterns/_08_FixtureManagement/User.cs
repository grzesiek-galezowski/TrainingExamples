namespace XUnitTestPatterns._08_FixtureManagement
{
  public class User //typically, DO NOT unit test data structures
  {
    public User(string name, string surname, int age)
    {
      Name = name;
      Surname = surname;
      Age = age;
    }

    public string Name { get; }
    public string Surname { get; }
    public int Age { get; }
  }
}
