using System;
using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;

namespace CombosNestedFunctions
{
  public class ComboSyntax
  {
    protected dynamic MoveList
    {
      get { return new ComboDynamicBuilder(); }
    }

    public class ComboDynamicBuilder : DynamicObject
    {
      public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
      {
        Console.Write("COMBO: ");
        Array.ForEach(binder.Name.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries), s => Console.Write(" " + s));
        Console.WriteLine();
        result = this;
        return true;
      }

    }

    [Test]
    public void ShouldBEHAVIOR()
    {
      new JinKazama().Combos();
      Assert.Fail("Unfinished");
    }
  }
}