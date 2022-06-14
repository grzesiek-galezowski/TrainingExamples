using System.Collections.Generic;
using System.Linq;

namespace XUnitTestPatterns._02_Basics
{
  public class DistinctFilter
  {
    public List<int> ApplyTo(params int[] args)
    {
      return args.Distinct().ToList();
    }

    public List<int> Apply3To(params int[] args)
    {
      return args.Distinct().Take(3).ToList();
    }
  }
}