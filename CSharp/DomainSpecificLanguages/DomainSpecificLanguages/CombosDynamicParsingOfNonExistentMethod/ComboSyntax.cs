using System;
using System.Dynamic;

namespace CombosDynamicParsingOfNonExistentMethod
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
  }
}