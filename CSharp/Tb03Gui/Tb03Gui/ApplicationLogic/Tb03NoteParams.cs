namespace Tb03Gui.ApplicationLogic;

public record Tb03NoteParams(bool Accent, bool Slide, int State)
{
  public static Tb03NoteParams From(bool accent, bool slide, int state)
  {
    return new Tb03NoteParams(accent, slide, state);
  }
}