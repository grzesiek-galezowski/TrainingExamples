import interfaces.GhostStates;
import other.*;
import states.AnimatedGhostStates;
import thirdpartyimpl.*;

/**
 * Created by astral on 11.11.2015.
 */
public class Main {

    public void Main() {
        RestoreTimer restoreTimer = new RestoreTimer();
        GhostStates states = new AnimatedGhostStates(
                new RealAppearance(),
                new AnimatedPacman(),
                new OnStageRestorePoint(),
                restoreTimer);
        Ghost ghost = new AnimatedGhost(states.chasing());

        restoreTimer.reportExpiryTo(ghost);
    }
}
