package _09_mock_objects;

import autofixture.publicinterface.Any;
import lombok.val;
import org.junit.jupiter.api.Test;

import static org.mockito.BDDMockito.given;
import static org.mockito.BDDMockito.then;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

public class Ex01BasicStubbingAndMocking {
    @Test
    public void shouldCopyDataFromSourceToDestination() {
        //GIVEN
        val copyOperation = new CopyOperation();
        val destination = mock(DataDestination.class);
        val source = mock(DataSource.class);
        val data = Any.anonymous(Data.class);

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
        val copyOperation = new CopyOperation();
        val destination = mock(DataDestination.class);
        val source = mock(DataSource.class);
        val data = Any.anonymous(Data.class);

        given(source.retrieveData()).willReturn(data);

        //WHEN
        copyOperation.applyTo(source, destination);

        //THEN
        then(destination).should().save(data);
    }
}
