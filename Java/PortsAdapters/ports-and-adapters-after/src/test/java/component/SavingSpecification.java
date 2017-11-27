package component;

import com.example.application.ApplicationLogic;
import com.example.ports.dataaccess.Repository;
import org.junit.Test;

import static org.mockito.BDDMockito.then;
import static org.mockito.Mockito.mock;

public class SavingSpecification {
  //bug this is not showing how to write good component specs
  //bug this is just an example showing that you can do this.

  @Test
  public void shouldSaveEmployeeInDatabaseWhenTriggeredFromView() {
    //GIVEN
    Repository persistentStorage = mock(Repository.class);
    ApplicationLogic app = new ApplicationLogic(persistentStorage);

    //THEN
    app.handleAddEmployeeRequest();

    //THEN
    then(persistentStorage).should().saveEmployee();
  }
}
