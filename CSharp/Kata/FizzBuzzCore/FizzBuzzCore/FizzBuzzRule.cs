namespace FizzBuzzCore
{
    public class FizzBuzzRule : IRule
    {
        public string Translate(int input)
        {
            return "FizzBuzz";
        }

        public bool AppliesTo(int input)
        {
            return input % 3 == 0 && input % 5 == 0;
        }
    }
}