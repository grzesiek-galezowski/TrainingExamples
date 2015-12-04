package states;

import interfaces.GhostState;
import thirdparty.InGameTimer;
import interfaces.GhostContext;
import interfaces.GhostStates;

/**
 * Created by astral on 11.11.2015.
 */
public class Restoring implements GhostState {
    private InGameTimer restoreTimer;
    private GhostStates states;

    public Restoring(InGameTimer restoreTimer, GhostStates states) {
        this.restoreTimer = restoreTimer;
        this.states = states;
    }

    @Override
    public void onEnter(GhostContext context) {
        restoreTimer.start();
    }

    @Override
    public void onUpdateMovement(GhostContext ghost) {
        //nothing
    }

    @Override
    public void onCollisionWithPacman(GhostContext ghost) {
        throw new RuntimeException("impossible");
    }

    @Override
    public void onPowerPillConsumedByPacman(GhostContext ghost) {

    }

    @Override
    public void onPillTimerFinished(GhostContext ghost) {
        //N/A
    }

    @Override
    public void onRestoreTimerFinished(GhostContext context) {
        context.changeStateTo(this.states.chasing());
    }

    @Override
    public void onRestorePointReached(GhostContext ghost) {
        throw new RuntimeException("Imporssible");
    }
}
