package p04_mockito;

import autofixture.publicinterface.Any;
import org.testng.annotations.Test;

import static org.mockito.BDDMockito.given;
import static org.mockito.Mockito.*;

public class Ex01BasicStubbingAndMocking {
  @Test
  public void shouldCopyDataFromSourceToDestination() {
    //GIVEN
    CopyOperation copyOperation = new CopyOperation();
    DataDestination destination = mock(DataDestination.class);
    DataSource source = mock(DataSource.class);
    Data data = Any.anonymous(Data.class);

    when(source.retrieveData()).thenReturn(data);

    //WHEN
    copyOperation.applyTo(source, destination);

    //THEN
    verify(destination).save(data);
    //verify(destination /*, atLeast(1)*/).save(data);
  }

  @Test
  public void shouldCopyDataFromSourceToDestination_bdd() {
    //GIVEN
    CopyOperation copyOperation = new CopyOperation();
    DataDestination destination = mock(DataDestination.class);
    DataSource source = mock(DataSource.class);
    Data data = Any.anonymous(Data.class);

    given(source.retrieveData()).willReturn(data);

    //WHEN
    copyOperation.applyTo(source, destination);

    //THEN
    verify(destination).save(data);
  }
}
