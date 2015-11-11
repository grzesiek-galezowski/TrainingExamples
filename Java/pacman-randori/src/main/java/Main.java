import interfaces.GhostStates;
import other.*;
import states.AnimatedGhostStates;
import thirdpartyimpl.*;

/**
 * Created by astral on 11.11.2015.
 */
public class Main {

    public void Main() {
        PillTimer pillTimer = new PillTimer();
        RestoreTimer restoreTimer = new RestoreTimer();
        GhostStates states = new AnimatedGhostStates(
                new RealAppearance(),
                new AnimatedPacman(),
                pillTimer,
                new OnStageRestorePoint(),
                restoreTimer);
        Ghost ghost = new Ghost(states.chasing(), states);

        pillTimer.reportExpiryTo(ghost);
        restoreTimer.reportExpiryTo(ghost);
    }
}
