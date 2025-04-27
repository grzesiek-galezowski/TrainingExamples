namespace FizzBuzz
{
  public interface Generator
  {
    string GenerateFrom(int input);
    bool Matches(int input);
  }

  public class DefaultGenerator : Generator
  {
    public string GenerateFrom(int input) => input.ToString();
    public bool Matches(int input) => true;
  }
}