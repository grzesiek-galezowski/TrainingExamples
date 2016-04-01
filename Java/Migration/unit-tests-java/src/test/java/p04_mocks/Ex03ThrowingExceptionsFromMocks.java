package p04_mocks;

import autofixture.publicinterface.Any;
import org.testng.annotations.Test;
import p04_mockito.CopyOperation;
import p04_mockito.Data;
import p04_mockito.DataDestination;
import p04_mockito.DataSource;

import static org.assertj.core.api.AssertionsForClassTypes.assertThatThrownBy;
import static org.mockito.Mockito.*;

public class Ex03ThrowingExceptionsFromMocks {

  @Test
  public void shouldThrowExceptionWhenReadingFromSourceThrowsException() {
    //GIVEN
    CopyOperation copyOperation = new CopyOperation();
    DataDestination destination = mock(DataDestination.class);
    DataSource source = mock(DataSource.class);
    RuntimeException exception = Any.anonymous(RuntimeException.class);

    when(source.retrieveData()).thenThrow(exception);

    //WHEN - THEN
    assertThatThrownBy(() -> copyOperation.applyTo(source, destination))
        .isEqualTo(exception);
    //never verification
    verify(destination, never()).save(any(Data.class));
  }

  @Test
  public void shouldThrowExceptionWhenSavingToDestinationThrowsException() {
    //GIVEN
    CopyOperation copyOperation = new CopyOperation();
    DataDestination destination = mock(DataDestination.class);
    DataSource source = mock(DataSource.class);
    Data data = Any.anonymous(Data.class);
    RuntimeException exception = Any.anonymous(RuntimeException.class);

    when(source.retrieveData()).thenReturn(data);
    doThrow(exception).when(destination).save(data);

    //WHEN - THEN
    assertThatThrownBy(() -> copyOperation.applyTo(source, destination))
        .isEqualTo(exception);
  }


}
