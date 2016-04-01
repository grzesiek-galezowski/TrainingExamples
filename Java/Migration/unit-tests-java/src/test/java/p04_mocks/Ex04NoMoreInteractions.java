package p04_mocks;

import org.testng.annotations.Test;
import p04_mockito.CommandInterpreter;
import p04_mockito.ExecutionEngine;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.verifyNoMoreInteractions;

public class Ex04NoMoreInteractions {

  @Test
  public void shouldTranslateShootingCommandToShootingReport() {
    //GIVEN
    ExecutionEngine executionEngine = mock(ExecutionEngine.class);
    CommandInterpreter interpreter = new CommandInterpreter(executionEngine);

    //WHEN
    interpreter.onEvent(CommandInterpreter.SHOOTING);

    //THEN
    verify(executionEngine).shootingStarted();
    verifyNoMoreInteractions(executionEngine);
  }

  @Test
  public void shouldTranslateRobberyCommandToRobberyReport() {
    //GIVEN
    ExecutionEngine executionEngine = mock(ExecutionEngine.class);
    CommandInterpreter interpreter = new CommandInterpreter(executionEngine);

    //WHEN
    interpreter.onEvent(CommandInterpreter.ROBBERY);

    //THEN
    verify(executionEngine).robberyTookPlace();
    verifyNoMoreInteractions(executionEngine);
  }

  @Test
  public void shouldTranslateIncidentCommandToIncidentReport() {
    //GIVEN
    ExecutionEngine executionEngine = mock(ExecutionEngine.class);
    CommandInterpreter interpreter = new CommandInterpreter(executionEngine);

    //WHEN
    interpreter.onEvent(CommandInterpreter.INCIDENT);

    //THEN
    verify(executionEngine).incidentDiscovered();
    verifyNoMoreInteractions(executionEngine /*, executionEngine, executionEngine */ );
  }
}
