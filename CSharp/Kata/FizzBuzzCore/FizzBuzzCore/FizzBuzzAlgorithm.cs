using System;

namespace FizzBuzzCore
{
    public class FizzBuzzAlgorithm
    {
        private readonly FizzRule _fizzRule = new FizzRule();
        private readonly BuzzRule _buzzRule = new BuzzRule();
        private readonly ToStringRule _toStringRule = new ToStringRule();
        private readonly FizzBuzzRule _fizzBuzzRule = new FizzBuzzRule();

        public string Translate(int input)
        {
            if (_fizzBuzzRule.AppliesTo(input))
            {
                return _fizzBuzzRule.Translate(input);
            }
            else
            {
                if (_fizzRule.AppliesTo(input))
                {
                    return _fizzRule.Translate(input);
                }
                else
                {
                    if (_buzzRule.AppliesTo(input))
                    {
                        return _buzzRule.Translate(input);
                    }
                    else
                    {
                        if (_toStringRule.AppliesTo(input))
                        {
                            return _toStringRule.Translate(input);
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }
                    
                }
            }

        }
    }
}