package p04_mockito;

public class CommandInterpreter {
  public static final String SHOOTING = "shooting";
  public static final String INCIDENT = "incident";
  public static final String ROBBERY = "robbery";
  private final ExecutionEngine executionEngine;

  public CommandInterpreter(ExecutionEngine executionEngine) {

    this.executionEngine = executionEngine;
  }

  public void onEvent(String command) {
    switch (command) {
      case SHOOTING:
        executionEngine.shootingStarted();
        break;
      case INCIDENT:
        executionEngine.incidentDiscovered();
        break;
      case ROBBERY:
        executionEngine.robberyTookPlace();
        break;
    }
  }
}
