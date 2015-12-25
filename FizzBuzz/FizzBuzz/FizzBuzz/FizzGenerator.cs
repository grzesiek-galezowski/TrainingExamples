namespace FizzBuzz
{
  class FizzGenerator : Generator
  {
    public string GenerateFrom(int input)
    {
      return "Fizz";
    }

    public bool Matches(int input)
    {
      return input % 3 == 0;
    }
  }
}