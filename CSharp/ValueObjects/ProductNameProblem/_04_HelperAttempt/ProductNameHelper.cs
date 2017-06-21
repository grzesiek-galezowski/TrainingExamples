using System;

namespace ProductNameProblem._04_HelperAttempt
{
  internal static class ProductNameHelper
  {
    public static bool AreProductNamesEqual(ProductTypes p1Type, string p1Name, ProductTypes p2Type, string p2Name)
    {
      return string.Equals(p2Name, p1Name, 
        StringComparison.InvariantCultureIgnoreCase)
        && p1Type == p2Type;
    }
  }
}