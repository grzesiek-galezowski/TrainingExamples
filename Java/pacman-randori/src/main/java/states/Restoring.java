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
    public void onEnter() {
        restoreTimer.start();
    }

    @Override
    public void onUpdateMovement() {
        //nothing
    }

    @Override
    public void onCollisionWithPacman(GhostContext context) {
        throw new RuntimeException("impossible");
    }

    @Override
    public void onPowerPillConsumedByPacman(GhostContext context) {

    }

    @Override
    public void onPillTimerFinished(GhostContext context) {
        //N/A
    }

    @Override
    public void onRestoreTimerFinished(GhostContext context) {
        context.changeStateTo(this.states.chasing());
    }

    @Override
    public void onRestorePointReached(GhostContext context) {
        throw new RuntimeException("Imporssible");
    }
}
