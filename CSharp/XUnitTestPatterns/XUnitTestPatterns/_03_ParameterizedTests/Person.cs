namespace XUnitTestPatterns._03_ParameterizedTests
{
  public class Person
  {
    public const int AdultAge = 18;
    private readonly int _age;

    public Person(int age)
    {
      _age = age;
    }

    public bool IsAdult()
    {
      return _age >= AdultAge;
    }
  }
}