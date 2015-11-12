import interfaces.GhostStates;
import main.GhostFactory;
import org.junit.Test;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

/**
 * Created by astral on 12.11.2015.
 */
public class GhostStateMachineSpecification {
    @Test
    public void shouldXyz() {
        ///TODO use real states!!!
        GhostStates states = mock(GhostStates.class);

        GhostFactory.createGhost(states);
    }
}
