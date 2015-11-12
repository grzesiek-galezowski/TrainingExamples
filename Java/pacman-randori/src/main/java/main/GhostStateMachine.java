package main;//TODO synchronization

import interfaces.GhostContext;
import interfaces.GhostState;
import interfaces.GhostStates;

/**
 * Created by astral on 11.11.2015.
 */
public class GhostStateMachine implements GhostContext, Ghost {

    private GhostState currentState;
    private GhostStates states;

    public GhostStateMachine(GhostState currentState, GhostStates states) {
        this.currentState = currentState;
        this.states = states;
    }

    @Override
    public void onUpdateMovement() {
        currentState.onUpdateMovement(this);
    }

    @Override
    public void onCollisionWithPacman() {
        currentState.onCollisionWithPacman(this);
    }

    @Override
    public void onPowerPillConsumedByPacman() {
        currentState.onPowerPillConsumedByPacman(this);
    }

    @Override
    public void onPillTimerFinished() {
        currentState.onPillTimerFinished(this);
    }

    @Override
    public void onRestoreTimerFinished() {
        currentState.onRestoreTimerFinished(this);
    }

    @Override
    public void onRestorePointReached() {
        currentState.onRestorePointReached(this);
    }

    @Override
    public void changeStateTo(GhostState ghostState) {
        this.currentState = ghostState;
        currentState.onEnter(this);
    }
}
