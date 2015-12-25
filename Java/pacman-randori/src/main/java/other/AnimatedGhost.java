package other;//TODO synchronization

import interfaces.GhostContext;
import interfaces.GhostState;

/**
 * Created by astral on 11.11.2015.
 */
public class AnimatedGhost implements GhostContext, Ghost {

    private GhostState currentState;

    public AnimatedGhost(GhostState currentState) {
        this.currentState = currentState;
    }

    public void start() {
        currentState.onEnter();
    }

    @Override
    public void onUpdateMovement() {
        currentState.onUpdateMovement();
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
    public void onPillEffectExpired() {
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
        ghostState.onEnter();
        this.currentState = ghostState;
    }
}
