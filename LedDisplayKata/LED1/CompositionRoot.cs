namespace LED1
{
  public static class CompositionRoot
  {
    public static Driver CreateDefaultDriver(Display display)
    {
      return new Driver(display,
        Row(_(), A(), _()),
        Row(F(), _(), B()),
        Row(_(), G(), _()),
        Row(E(), _(), C()),
        Row(_(), D(), _()));
    }

    private static Row Row(Displayable a1, Displayable a2, Displayable a3)
    {
      return new Row(a1, a2, a3);
    }

    private static Displayable _()
    {
      return new BlankSpace();
    }

    private static Displayable D()
    {
      return SwitchableSegment2('D', "-", "*");
    }


    private static Displayable C()
    {
      return SwitchableSegment2('C', "|", "*");
    }

    private static Displayable E()
    {
      return SwitchableSegment2('E', "|", "*");
    }

    private static Displayable G()
    {
      return SwitchableSegment2('G', "-", "*");
    }

    private static Displayable B()
    {
      return SwitchableSegment2('B', "|", "*");
    }

    private static Displayable A()
    {
      return SwitchableSegment2('A', "-", "*");
    }

    private static Displayable F()
    {
      return SwitchableSegment2('F', "|", "*");
    }

    private static Displayable SwitchableSegment2(char onTrigger, string firstOnValue, string secondOnValue)
    {
      return new CircularDisplayable(
        SegmentFactory.CreateSwitchable(onTrigger, firstOnValue, _()),
        SegmentFactory.CreateSwitchable(onTrigger, secondOnValue, _()),
        SegmentFactory.CreateSwitchable(onTrigger, onTrigger.ToString(), _()));
    }

  }
}