namespace DigitsRandori
{
  public class Row
  {
    private readonly Cell _cell1;
    private readonly Cell _cell2;
    private readonly Cell _cell3;

    public Row(Cell cell1, Cell cell2, Cell cell3)
    {
      _cell1 = cell1;
      _cell2 = cell2;
      _cell3 = cell3;
    }

    public string Light(int[] ints)
    {
      return _cell1.LightAccordingTo(ints) + _cell2.LightAccordingTo(ints) + _cell3.LightAccordingTo(ints);
    }
  }
}