package _09_mock_objects;

import lombok.val;
import org.testng.annotations.Test;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.verifyNoMoreInteractions;

public class Ex04NoMoreInteractions {

  @Test
  public void shouldTranslateShootingCommandToShootingReport() {
    //GIVEN
    val executionEngine = mock(ExecutionEngine.class);
    val interpreter = new CommandInterpreter(executionEngine);

    //WHEN
    interpreter.onEvent(CommandInterpreter.SHOOTING);

    //THEN
    verify(executionEngine).shootingStarted();
    verifyNoMoreInteractions(executionEngine);
  }

  @Test
  public void shouldTranslateRobberyCommandToRobberyReport() {
    //GIVEN
    val executionEngine = mock(ExecutionEngine.class);
    val interpreter = new CommandInterpreter(executionEngine);

    //WHEN
    interpreter.onEvent(CommandInterpreter.ROBBERY);

    //THEN
    verify(executionEngine).robberyTookPlace();
    verifyNoMoreInteractions(executionEngine);
  }

  @Test
  public void shouldTranslateIncidentCommandToIncidentReport() {
    //GIVEN
    val executionEngine = mock(ExecutionEngine.class);
    val interpreter = new CommandInterpreter(executionEngine);

    //WHEN
    interpreter.onEvent(CommandInterpreter.INCIDENT);

    //THEN
    verify(executionEngine).incidentDiscovered();
    verifyNoMoreInteractions(executionEngine /*, executionEngine, executionEngine */ );
  }
}
