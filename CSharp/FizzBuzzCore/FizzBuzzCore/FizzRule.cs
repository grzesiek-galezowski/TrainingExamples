namespace FizzBuzzCore
{
    public class FizzRule
    {
        public string Translate(int input)
        {
            return "Fizz";
        }

        public bool AppliesTo(int input)
        {
            return input % 3 == 0;
        }
    }
}