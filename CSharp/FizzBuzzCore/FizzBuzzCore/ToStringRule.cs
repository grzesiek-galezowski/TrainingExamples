namespace FizzBuzzCore
{
    public class ToStringRule : IRule
    {
        public string Translate(int input)
        {
            return input.ToString();
        }

        public bool AppliesTo(int input)
        {
            return true;
        }
    }
}