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
    private InGameTimer pillTimer;
    private GhostStates states;

    public Restoring(InGameTimer restoreTimer, InGameTimer pillTimer, GhostStates states) {
        this.restoreTimer = restoreTimer;
        this.pillTimer = pillTimer;
        this.states = states;
    }

    @Override
    public void onEnter(GhostContext context) {
        restoreTimer.restart();
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
        pillTimer.restart();
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
