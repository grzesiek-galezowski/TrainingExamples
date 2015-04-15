using System;
using System.Collections.Generic;

namespace CombosNestedFunctions
{
  public class ComboBuilder
  {
    private readonly List<ComboData> _combos = new List<ComboData>();
    private ComboData _currentCombo;
    private bool _plusFlag = false;

    public ComboBuilder(string firstComboName)
    {
      _currentCombo = new ComboData(firstComboName);
      _combos.Add(_currentCombo);
    }

    private ComboBuilder RegisterKeyPress(int keyCode)
    {
      var keyPress = new KeyPress(keyCode);
      if (_plusFlag)
      {
        _currentCombo.AddPressedTogetherWithPrevious(keyCode);
        _plusFlag = false;
      }
      else
      {
        _currentCombo.Add(keyPress);
      }
      return this;
    }



    public ComboBuilder RP
    {
      get
      {
        return RegisterKeyPress(0x01);
      }
    }




    public ComboBuilder RK
    {
      get
      {
        return RegisterKeyPress(0x02);
      }
    }

    public ComboBuilder LP
    {
      get
      {
        return RegisterKeyPress(0x03);
      }
    }

    public ComboBuilder LK
    {
      get
      {
        return RegisterKeyPress(0x04);
      }
    }

    public ComboBuilder f
    {
      get
      {
        return RegisterKeyPress(0x05);
      }
    }

    public ComboBuilder b
    {
      get
      {
        return RegisterKeyPress(0x06);
      }
    }

    public ComboBuilder u
    {
      get
      {
        return RegisterKeyPress(0x07);
      }
    }

    public ComboBuilder d
    {
      get
      {
        return RegisterKeyPress(0x08);
      }
    }

    public ComboBuilder N
    {
      get
      {
        return RegisterKeyPress(0x09);
      }
    }

    public ComboBuilder plus
    {
      get 
      { 
        _plusFlag = true;
        return this;
      }
    }

    public List<ComboData> End
    {
      get { return _combos; }
    }

    public static implicit operator List<ComboData>(ComboBuilder builder)
    {
      return builder.End;
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

    public ComboBuilder Combo(string name)
    {
      _currentCombo = new ComboData(name);
      _combos.Add(_currentCombo);
      return this;
    }
  }

  public class ComboData
  {
    public ComboData(string name)
    {
      Name = name;
    }

    private readonly List<ComboBuilder.KeyPress> _keys = new List<ComboBuilder.KeyPress>();
    private string Name { get; set; }

    private List<ComboBuilder.KeyPress> Keys
    {
      get { return _keys; }
    }

    private ComboBuilder.KeyPress LastKeyPress
    {
      get { return _keys[_keys.Count - 1]; }
      set { _keys[_keys.Count - 1] = value; }
    }


    public void Add(ComboBuilder.KeyPress keyPress)
    {
      _keys.Add(keyPress);
    }

    public void AddPressedTogetherWithPrevious(int keyCode)
    {
      LastKeyPress = new ComboBuilder.KeyPress(LastKeyPress, new ComboBuilder.KeyPress(keyCode));
    }
  }
}