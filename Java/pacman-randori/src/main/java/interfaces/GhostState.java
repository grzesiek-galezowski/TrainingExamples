package interfaces;

/**
 * Created by astral on 11.11.2015.
 */
public interface GhostState {
    void onEnter();

    void onUpdateMovement();

    void onCollisionWithPacman(GhostContext context);

    void onPowerPillConsumedByPacman(GhostContext context);

    void onPillTimerFinished(GhostContext context);

    void onRestoreTimerFinished(GhostContext context);

    void onRestorePointReached(GhostContext context);
}
