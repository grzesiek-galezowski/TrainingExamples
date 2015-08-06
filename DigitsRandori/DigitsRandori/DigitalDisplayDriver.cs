namespace DigitsRandori
{
  public class DigitalDisplayDriver
  {
    private readonly Display _realDisplay;
    private readonly Row _row1;
    private readonly Row _row2;
    private readonly Row _row3;
    private readonly Row _row4;
    private readonly Row _row5;

    public DigitalDisplayDriver(Display realDisplay, Row row1, Row row2, Row row3, Row row4, Row row5)
    {
      _realDisplay = realDisplay;
      _row1 = row1;
      _row2 = row2;
      _row3 = row3;
      _row4 = row4;
      _row5 = row5;
    }

    public void Send(params int[] bulbs)
    {
      _realDisplay.Put(
        _row1.Light(bulbs),
        _row2.Light(bulbs),
        _row3.Light(bulbs),
        _row4.Light(bulbs),
        _row5.Light(bulbs));
    }
  }
}