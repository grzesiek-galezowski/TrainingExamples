using System;

namespace FizzBuzz
{
  class FizzBuzzAlgorithm
  {
    private readonly Generator _fizzBuzzConverter = new FizzBuzzConverter();
    private readonly Generator _fizzGenerator = new FizzGenerator();
    private readonly Generator _buzzGenerator = new BuzzGenerator();
    private readonly Generator _defaultGenerator = new DefaultGenerator();

    public string Convert(int input)
    {
      var generators = new[] { _fizzBuzzConverter, _fizzGenerator, _buzzGenerator, _defaultGenerator };

      foreach (var generator in generators)
      {
        if (generator.Matches(input))
        {
          return generator.GenerateFrom(input);
        }
      }

      throw new NotSupportedException();
    }
  }
}