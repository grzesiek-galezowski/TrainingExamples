package _09_mock_objects;

import autofixture.publicinterface.Any;
import lombok.val;
import org.mockito.InOrder;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Mockito.inOrder;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

public class Ex02MultipleReturnsAndOrderVerification {
    @Test
    public void shouldReturnMultipleDataElements() {
        //GIVEN
        val source = mock(DataSource.class);
        val data1 = Any.anonymous(Data.class);
        val data2 = Any.anonymous(Data.class);

        when(source.retrieveData())
            .thenReturn(data1)
            .thenReturn(data2);

        //WHEN
        val result1 = source.retrieveData();
        val result2 = source.retrieveData();

        //THEN
        assertThat(result1).isEqualTo(data1);
        assertThat(result2).isEqualTo(data2);
    }

    @Test //order verification
    public void shouldCopyDataFromSourceToDestinationMultipleTimes() {
        //GIVEN
        CopyOperation copyOperation = new CopyOperation();
        DataDestination destination = mock(DataDestination.class);
        DataSource source = mock(DataSource.class);
        Data data1 = Any.anonymous(Data.class);
        Data data2 = Any.anonymous(Data.class);


        when(source.retrieveData()).thenReturn(data1).thenReturn(data2);

        //WHEN
        copyOperation.applyTo(source, destination);
        copyOperation.applyTo(source, destination);

        //THEN
        InOrder inOrder = inOrder(destination);
        inOrder.verify(destination).save(data1);
        inOrder.verify(destination).save(data2);
    }
}
