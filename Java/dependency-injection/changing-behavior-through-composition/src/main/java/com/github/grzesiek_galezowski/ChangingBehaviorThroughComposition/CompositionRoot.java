package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition;

import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarms.*;
import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriterias.CompoundSwitchCriteria;
import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriterias.MetDuringTheDayCriteria;
import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriterias.MetOnWorkDayCriteria;

public class CompositionRoot {
  private static final String SecurityPhoneNumber = "11-222-1121";
  private static final String PolicePhoneNumber = "122-33-22-12";

  public void aLoudAlarm() {
    Building building = new Building(new LoudAlarm());
  }

  public void aSilentAlarm() {
    Building building = new Building(new SilentAlarm(PolicePhoneNumber));
  }

  public void alarmDisabled() {
    Building building = new Building(new NoAlarm());
  }

  public void twoSilentAlarms() {
    Building building = new Building(
        new CompoundAlarm(
            new SilentAlarm(PolicePhoneNumber),
            new SilentAlarm(SecurityPhoneNumber)));
  }

  public void twoSilentAlarmsAndOneLoud() {
    Building building = new Building(
        new CompoundAlarm(
            new SilentAlarm(PolicePhoneNumber),
            new SilentAlarm(SecurityPhoneNumber),
            new LoudAlarm()));
  }

  public void twoSilentAlarmsAndOneLoudAtNightAndNoneDuringDay() {
    Building building = new Building(
        new SwitchableAlarm(new MetDuringTheDayCriteria(),
            new NoAlarm(),
            new CompoundAlarm(
                new SilentAlarm(PolicePhoneNumber),
                new SilentAlarm(SecurityPhoneNumber),
                new LoudAlarm())
        ));
  }

  public void byDefaultNoAlarmButTwoSilentAlarmsAndOneLoudOnlyAtNightWeekend() {
    Building building = new Building(
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

  public void byDefaultNoAlarmButTwoSilentAlarmsAndOneLoudOnlyAtNightWeekendInitializedInParallel() {
    Building building = new Building(
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

  /* Conclusions:
   * 1. Changing behavior by composition
   * 2. Did not have to modify any code but composition
   * 3. Separate use from construction - SRP
   */

  public void byDefaultNoAlarmButTwoSilentAlarmsAndOneLoudOnlyAtNightWeekendInitializedInParallelFluentInterface() {
    Building building = new Building(
        switchBetween(
            doNothing(), ifBoth(isDay(), isNotWeekend()),
            otherwise(
                call(PolicePhoneNumber),
                call(SecurityPhoneNumber),
                activateSirens())));
  }

  private static NoAlarm doNothing() {
    return new NoAlarm();
  }

  private static LoudAlarm activateSirens() {
    return new LoudAlarm();
  }

  private Alarm call(String policePhoneNumber) {
    return new SilentAlarm(policePhoneNumber);
  }

  private Alarm otherwise(Alarm... alarms) {
    return new ParallelCompoundAlarm(alarms);
  }

  private static MetOnWorkDayCriteria isNotWeekend() {
    return new MetOnWorkDayCriteria();
  }

  private static MetDuringTheDayCriteria isDay() {
    return new MetDuringTheDayCriteria();
  }

  private SwitchCriteria ifBoth(SwitchCriteria ...criterias) {
    return new CompoundSwitchCriteria(criterias);
  }

  public SwitchableAlarm switchBetween(Alarm defaultAlarm, SwitchCriteria switchCriteria, Alarm secondaryAlarm) {
    return new SwitchableAlarm(switchCriteria, defaultAlarm, secondaryAlarm);
  }
}
