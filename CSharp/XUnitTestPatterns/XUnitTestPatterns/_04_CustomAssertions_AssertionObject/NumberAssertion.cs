using System;

namespace XUnitTestPatterns._04_CustomAssertions_AssertionObject
{
  public class NumberAssertion
  {
    public void ApplyTo(int i)
    {
      if (i < 0)
      {
        throw new InvalidOperationException("Trolololo");
      }
    }
  }
}