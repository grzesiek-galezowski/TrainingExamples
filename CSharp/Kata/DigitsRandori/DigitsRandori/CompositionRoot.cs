namespace DigitsRandori
{
  static internal class CompositionRoot
  {
    public static DigitalDisplayDriver CreateDriver(Display realDisplay)
    {
      return new DigitalDisplayDriver(realDisplay,
        new Row(Off(), H(0), Off()),
        new Row(V(1), Off(), V(2)),
        new Row(Off(), H(3), Off()),
        new Row(V(4), Off(), V(5)),
        new Row(Off(), H(6), Off())
        );
    }

    private static LightableCell V(int lightOnCode)
    {
      return LightableCell.Vertical(lightOnCode);
    }

    private static LightableCell H(int lightOnCode)
    {
      return LightableCell.Horizontal(lightOnCode);
    }

    private static Cell Off()
    {
      return StaleCell.Off();
    }
  }
}