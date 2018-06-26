package _09_mock_objects;

import autofixture.publicinterface.Any;
import lombok.val;
import org.testng.annotations.Test;

import static org.assertj.core.api.AssertionsForClassTypes.assertThatThrownBy;
import static org.mockito.Mockito.any;
import static org.mockito.Mockito.doThrow;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.never;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

public class Ex03ThrowingExceptionsFromMocks {

    @Test
    public void shouldThrowExceptionWhenReadingFromSourceThrowsException() {
        //GIVEN
        val copyOperation = new CopyOperation();
        val destination = mock(DataDestination.class);
        val source = mock(DataSource.class);
        val exception = Any.runtimeException();

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
        val copyOperation = new CopyOperation();
        val destination = mock(DataDestination.class);
        val source = mock(DataSource.class);
        val data = Any.anonymous(Data.class);
        val exception = Any.runtimeException();

        when(source.retrieveData()).thenReturn(data);
        doThrow(exception).when(destination).save(data);

        //WHEN - THEN
        assertThatThrownBy(() -> copyOperation.applyTo(source, destination))
            .isEqualTo(exception);
    }


}
