namespace CombosFunctionSequence
{
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

    public static KeyPress operator +(KeyPress press1, KeyPress press2)
    {
      return new KeyPress(press1._keyCode | press2._keyCode);
    }

    public static KeyPress operator /(KeyPress press1, KeyPress press2)
    {
      return new KeyPress(press1._keyCode | press2._keyCode);
    }

  }
}