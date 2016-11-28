namespace ChangingBehaviorThroughComposition
{
  public class CompositionRoot
  {
    private const string SecurityPhoneNumber = "11-222-1121";
    private const string PolicePhoneNumber = "122-33-22-12";

    #region hide
    public void ALoudAlarm()
    {
      var building = new Building(new LoudAlarm());
    }
    #endregion

    #region hide
    public void ASilentAlarm()
    {
      var building = new Building(new SilentAlarm(PolicePhoneNumber));
    }
    #endregion

    #region hide
    public void AlarmDisabled()
    {
      var building = new Building(new NoAlarm());
    }
    #endregion

    #region hide
    public void TwoSilentAlarms()
    {
      var building = new Building(
        new CompoundAlarm(
          new SilentAlarm(PolicePhoneNumber),
          new SilentAlarm(SecurityPhoneNumber)));
    }
    #endregion

    #region hide
    public void TwoSilentAlarmsAndOneLoud()
    {
      var building = new Building(
        new CompoundAlarm(
          new SilentAlarm(PolicePhoneNumber),
          new SilentAlarm(SecurityPhoneNumber),
          new LoudAlarm()));
    }
    #endregion

    #region hide
    public void TwoSilentAlarmsAndOneLoudAtNightAndNoneDuringDay()
    {
      var building = new Building(
        new SwitchableAlarm(new MetDuringTheDayCriteria(), 
          new NoAlarm(),
          new CompoundAlarm(
            new SilentAlarm(PolicePhoneNumber),
            new SilentAlarm(SecurityPhoneNumber),
            new LoudAlarm())
          ));
    }
    #endregion

    #region hide
    public void ByDefaultNoAlarmButTwoSilentAlarmsAndOneLoudOnlyAtNightWeekend()
    {
      var building = new Building(
        new SwitchableAlarm(
          new CompoundSwitchCriteria(
            new MetDuringTheDayCriteria(), 
            new MetOnWorkDayCriteria()),
          new NoAlarm(),
          new CompoundAlarm(
            new SilentAlarm(PolicePhoneNumber),
            new SilentAlarm(SecurityPhoneNumber),
            new LoudAlarm())
          ));
    }
    #endregion

    #region hide
    public void ByDefaultNoAlarmButTwoSilentAlarmsAndOneLoudOnlyAtNightWeekend__InitializedInParallel()
    {
      var building = new Building(
        new SwitchableAlarm(
          new CompoundSwitchCriteria(
            new MetDuringTheDayCriteria(), 
            new MetOnWorkDayCriteria()),
          new NoAlarm(),
          new ParallelCompoundAlarm( //!!!
            new SilentAlarm(PolicePhoneNumber),
            new SilentAlarm(SecurityPhoneNumber),
            new LoudAlarm())
          ));
    }
    #endregion

  
  /* Conclusions:
   * 1. Changing behavior by composition
   * 2. Did not have to modify any code but composition
   * 3. Separate use from construction - SRP
   */

    #region hide - DSL - show later!
    public void ByDefaultNoAlarmButTwoSilentAlarmsAndOneLoudOnlyAtNightWeekend__InitializedInParallel_FluentInterface()
    {
      var building = new Building(
        Switch(
          IfBoth(IsDay(), IsNotWeekend()), 
            DoNothing(),
          Else(
            Call(PolicePhoneNumber),
            Call(SecurityPhoneNumber),
            ActivateSirens())));
    }

    private static NoAlarm DoNothing()
    {
      return new NoAlarm();
    }

    private static LoudAlarm ActivateSirens()
    {
      return new LoudAlarm();
    }

    private Alarm Call(string policePhoneNumber)
    {
      return new SilentAlarm(policePhoneNumber);
    }

    private Alarm Else(params Alarm[] alarms)
    {
      return new ParallelCompoundAlarm(alarms);
    }

    private static MetOnWorkDayCriteria IsNotWeekend()
    {
      return new MetOnWorkDayCriteria();
    }

    private static MetDuringTheDayCriteria IsDay()
    {
      return new MetDuringTheDayCriteria();
    }

    private SwitchCriteria IfBoth(params SwitchCriteria[] criterias)
    {
      return new CompoundSwitchCriteria(criterias);
    }


    public SwitchableAlarm Switch(SwitchCriteria switchCriteria, Alarm defaultAlarm, Alarm secondaryAlarm)
    {
      return new SwitchableAlarm(switchCriteria, defaultAlarm, secondaryAlarm);
    }


    #endregion
  }
}
