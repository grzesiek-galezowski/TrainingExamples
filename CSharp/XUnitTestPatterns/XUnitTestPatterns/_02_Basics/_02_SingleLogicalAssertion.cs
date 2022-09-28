using System;
using System.Collections.Generic;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace XUnitTestPatterns._02_Basics
{
  public class _02_SingleLogicalAssertion
  {
    //example 1: multiple runtime assertions
    [Test]
    public void ShouldLeaveUniqueItems()
    {
      //GIVEN
      var distinctFilter = new DistinctFilter();

      //WHEN
      var result = distinctFilter.ApplyTo(1, 2, 3, 3, 4, 3, 3);

      //THEN
      AssertHasUniqueItems(result);
    }

    //example 2: multiple in-code assertions
    [Test]
    public void ShouldLeaveLast3UniqueItems()
    {
      //GIVEN
      var distinctFilter = new DistinctFilter();

      //WHEN
      var result = distinctFilter.Apply3To(1, 2, 3, 3, 4, 3, 3);

      //THEN
      Assert.AreNotEqual(result[1], result[0]);
      Assert.AreNotEqual(result[2], result[0]);
      Assert.AreNotEqual(result[2], result[1]);
    }

    //violation
    [Test]
    public void ShouldReportItCanHandleStringWithLengthOf3ButNotOf4AndNotNullString()
    {
      //GIVEN
      var bufferSizeRule = new BufferSizeRule();

      //WHEN
      var resultForLengthOf3 = bufferSizeRule.CanHandle(Any.String(3));

      //THEN
      Assert.True(resultForLengthOf3);

      //WHEN - again?
      var resultForLengthOf4 = bufferSizeRule.CanHandle(Any.String(4));

      //THEN - again?
      Assert.False(resultForLengthOf4);

      //WHEN - again??
      var resultForNull = bufferSizeRule.CanHandle(null);

      //THEN - again??
      Assert.False(resultForNull);
    }


    public static void AssertHasUniqueItems<T>(List<T> list)
    {
      for (var i = 0; i < list.Count; i++)
      {
        for (var j = 0; j < list.Count; j++)
        {
          if (i != j) // conditional logic in tests?
          {
            Console.WriteLine("assertion!");
            Assert.AreNotEqual(list[j], list[i]);
          }
        }
      }
    }
  }
}