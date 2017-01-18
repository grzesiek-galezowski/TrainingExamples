using System;
using System.Linq;

namespace CombosClassicNestedFunctions
{
  public class ComboSyntax
  {
    protected static KeyPress RP()
    {
      return new KeyPress(0x01);
    }

    protected static KeyPress RK()
    {
      return new KeyPress(0x02);
    }

    protected static KeyPress LP()
    {
      return new KeyPress(0x03);
    }

    protected static KeyPress LK()
    {
      return new KeyPress(0x04);
    }

    protected static KeyPress f()
    {
      return new KeyPress(0x05);
    }

    protected static KeyPress b()
    {
      return new KeyPress(0x06);
    }

    protected static KeyPress u()
    {
      return new KeyPress(0x07);
    }

    protected static KeyPress d()
    {
      return new KeyPress(0x08);
    }

    protected static KeyPress N()
    {
      return new KeyPress(0x09);
    }

    public class KeyPress
    {
      private readonly int _keyCode;

      public KeyPress(int keyCode)
      {
        _keyCode = keyCode;
      }

      public KeyPress Join(KeyPress press2)
      {
        return new KeyPress(_keyCode | press2._keyCode);
      }
       
    }

    protected static void Combo(string name, params KeyPress[] keySequence)
    {
      throw new NotImplementedException();
    }

    protected KeyPress All(params KeyPress[] keys)
    {
      return keys.Aggregate((k1, k2) => k1.Join(k2));
    }

    protected KeyPress All(KeyPress key1, KeyPress key2)
    {
      return key1.Join(key2);
    }
  }
}