namespace FizzBuzz
{
  class FizzBuzzConverter : Generator
  {

    public bool Matches(int input) => input%3 == 0 && input%5 == 0;

    public string GenerateFrom(int input)
    {
      return "FizzBuzz";
    }
  }
}