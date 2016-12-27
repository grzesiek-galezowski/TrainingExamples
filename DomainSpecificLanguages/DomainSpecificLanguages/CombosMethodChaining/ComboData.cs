using System.Collections.Generic;

namespace CombosMethodChaining
{
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