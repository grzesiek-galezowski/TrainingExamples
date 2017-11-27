package component;

import com.example.application.ApplicationLogic;
import com.example.ports.dataaccess.Repository;
import org.junit.Test;
import org.mockito.Mockito;

import static org.mockito.BDDMockito.then;

public class SavingSpecification {

  public class TheOnlyPathSpecification {
    @Test
    //bug this is not showing how to write good component specs
    //bug this is just an example showing that you can do this.
    public void shouldSaveEmployeeInDatabaseWhenTriggeredFromView() {
      //GIVEN
      Repository persistentStorage = Mockito.mock(Repository.class);
      ApplicationLogic app = new ApplicationLogic(persistentStorage);

      //THEN
      app.handleAddEmployeeRequest();

      //THEN
      then(persistentStorage).should().saveEmployee();
    }
  }

}
