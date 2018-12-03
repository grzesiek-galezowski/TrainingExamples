namespace FizzBuzzCore
{
    public class BuzzRule : IRule
    {
        public string Translate(int input)
        {
            return "Buzz";
        }

        public bool AppliesTo(int input)
        {
            return input % 5 == 0;
        }
    }
}