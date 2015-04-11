using System;
using System.Collections.Generic;

namespace CombosNestedFunctions
{
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

    protected static ComboData Combo(string name, params KeyPress[] keySequence)
    {
      var data = new ComboData(name);
      foreach (var keyPress in keySequence)
      {
        data.Add(keyPress);
      }

      return data;
    }

    public List<ComboData> MoveList(params ComboData[] combos)
    {
      return new List<ComboData>(combos);
    }
  }

  public class KeyPress
  {
    private readonly int _keyCode;

    public KeyPress(int keyCode)
    {
      _keyCode = keyCode;
    }

    public KeyPress(KeyPress lastKeyPress, KeyPress keyPress)
    {
      _keyCode = lastKeyPress._keyCode | keyPress._keyCode;
    }

    public static KeyPress operator+(KeyPress press1, KeyPress press2)
    {
      return new KeyPress(press1._keyCode | press2._keyCode);
    }

    public static KeyPress operator/(KeyPress press1, KeyPress press2)
    {
      return new KeyPress(press1._keyCode | press2._keyCode);
    }
        
  }

  public class ComboData
  {
    public ComboData(string name)
    {
      Name = name;
    }

    private readonly List<KeyPress> _keys = new List<KeyPress>();
    private string Name { get; set; }

    private List<KeyPress> Keys
    {
      get { return _keys; }
    }

    private KeyPress LastKeyPress
    {
      get { return _keys[_keys.Count - 1]; }
      set { _keys[_keys.Count - 1] = value; }
    }


    public void Add(KeyPress keyPress)
    {
      _keys.Add(keyPress);
    }

    public void AddPressedTogetherWithPrevious(int keyCode)
    {
      LastKeyPress = new KeyPress(LastKeyPress, new KeyPress(keyCode));
    }
  }

}