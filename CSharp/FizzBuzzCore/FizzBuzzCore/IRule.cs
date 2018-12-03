namespace FizzBuzzCore
{
    public interface IRule
    {
        string Translate(int input);
        bool AppliesTo(int input);
    }
}