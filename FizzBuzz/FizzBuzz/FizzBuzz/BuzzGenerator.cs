namespace FizzBuzz
{
  class BuzzGenerator : Generator
  {
    public bool Matches(int input)
    {
      return input % 5 == 0;
    }

    public string GenerateFrom(int input)
    {
      return "Buzz";
    }
  }
}