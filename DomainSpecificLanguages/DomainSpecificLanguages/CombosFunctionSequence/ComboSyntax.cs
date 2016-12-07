using System.Collections.Generic;

namespace CombosFunctionSequence
{
  public class ComboSyntax
  {
    private bool _hold;
    private ComboData _currentCombo;
    private List<ComboData> _combos;

    protected const bool Hold = true;

    protected void d(bool hold = false)
    {
      RegisterKeyPress(0x01);
      _hold = hold;
    }

    protected void N()
    {
      RegisterKeyPress(0x00);
    }

    protected void LK()
    {
      RegisterKeyPress(0x02);
    }

    protected void b(bool hold = false)
    {
      RegisterKeyPress(0x03);
      _hold = hold;
    }

    protected void f(bool hold = false)
    {
      RegisterKeyPress(0x05);
      _hold = hold;
    }

    protected void RP()
    {
      RegisterKeyPress(0x06);
    }

    protected void RK()
    {
      RegisterKeyPress(0x07);
    }

    protected void LP(bool hold = false)
    {
      RegisterKeyPress(0x08);
      _hold = hold;
    }

    protected void Combo(string name)
    {
      _currentCombo = new ComboData(name);
      _combos.Add(_currentCombo);
    }

    private void RegisterKeyPress(int keyCode)
    {
      var keyPress = new KeyPress(keyCode);
      if (_hold)
      {
        _currentCombo.AddPressedTogetherWithPrevious(keyCode);
        _hold = false;
      }
      else
      {
        _currentCombo.Add(keyPress);
      }

    }

  }
}