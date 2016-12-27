using System;

namespace CombosExtensionMethods
{
  public static class ComboSyntaxExtensions
  {
    public static void Sequence(this string name, params KeyPress[] keySequence)
    {
      throw new NotImplementedException();
    }

    public static KeyPress Plus(this KeyPress key1, KeyPress key2, KeyPress key3)
    {
      return key1.Join(key2).Join(key3);
    }

    public static KeyPress Plus(this KeyPress key1, KeyPress key2)
    {
      return key1.Join(key2);
    }
  }

  public class ComboSyntax
  {
    protected static KeyPress RP
    {
      get { return new KeyPress(0x01); }
    }

    protected static KeyPress RK
    {
      get { return new KeyPress(0x02); }
    }

    protected static KeyPress LP
    {
      get { return new KeyPress(0x03); }
    }

    protected static KeyPress LK
    {
      get { return new KeyPress(0x04); }
    }

    protected static KeyPress f
    {
      get { return new KeyPress(0x05); }
    }

    protected static KeyPress b
    {
      get { return new KeyPress(0x06); }
    }

    protected static KeyPress u
    {
      get { return new KeyPress(0x07); }
    }

    protected static KeyPress d
    {
      get { return new KeyPress(0x08); }
    }

    protected static KeyPress N
    {
      get { return new KeyPress(0x09); }
    }
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
}