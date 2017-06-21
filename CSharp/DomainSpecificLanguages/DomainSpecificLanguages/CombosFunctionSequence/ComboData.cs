using System.Collections.Generic;

namespace CombosFunctionSequence
{
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