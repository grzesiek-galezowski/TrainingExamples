package alarms;

import interfaces.Alarm;
import adapters.TelephoneService;

public class SilentAlarm implements Alarm {
  private final String _numberToCall;

  public SilentAlarm(String numberToCall) {
    _numberToCall = numberToCall;
  }

  public void trigger() {
    TelephoneService.call(_numberToCall);
  }

  public void disable() {
    TelephoneService.recall(_numberToCall);
  }

  public void dump() {
    System.out.println("{ Calls: " + _numberToCall + " }");
  }
}
