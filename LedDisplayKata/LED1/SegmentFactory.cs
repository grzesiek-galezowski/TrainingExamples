namespace LED1
{
  public static class SegmentFactory
  {
    public static Displayable CreateSwitchable(char onTrigger, string onValue, Displayable fallbackDisplayable)
    {
      return new SwitchableSegment(onTrigger, onValue, fallbackDisplayable);
    }
  }
}