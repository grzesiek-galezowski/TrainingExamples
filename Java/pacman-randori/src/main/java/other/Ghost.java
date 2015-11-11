package other;//TODO synchronization

import interfaces.GhostContext;
import interfaces.GhostState;
import interfaces.GhostStates;

/**
 * Created by astral on 11.11.2015.
 */
public class Ghost implements GhostContext {

    private GhostState currentState;
    private GhostStates states;

    public Ghost(GhostState currentState, GhostStates states) {
        this.currentState = currentState;
        this.states = states;
    }

    public void onUpdateMovement() {
        currentState.onUpdateMovement(this);
    }

    public void onCollisionWithPacman() {
        currentState.onCollisionWithPacman(this);
    }

    public void onPowerPillConsumedByPacman() {
        currentState.onPowerPillConsumedByPacman(this);
    }

    public void onPillTimerFinished() {
        currentState.onPillTimerFinished(this);
    }

    public void onRestoreTimerFinished() {
        currentState.onRestoreTimerFinished(this);
    }

    public void onRestorePointReached() {
        currentState.onRestorePointReached(this);
    }

    @Override
    public void changeStateTo(GhostState ghostState) {
        this.currentState = ghostState;
        currentState.onEnter(this);
    }
}
