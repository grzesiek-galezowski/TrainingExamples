namespace StateWithImplicitContextPassing.Interfaces
{
  public interface LightSwitchStates
  {
    LightSwitchState Initial(LightSwitchContext context);
    LightSwitchState SwitchedOff(LightSwitchContext context);
    LightSwitchState SwitchedOn(LightSwitchContext context);
  }
}