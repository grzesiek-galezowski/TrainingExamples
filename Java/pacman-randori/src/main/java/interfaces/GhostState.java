package interfaces;

/**
 * Created by astral on 11.11.2015.
 */
public interface GhostState {
    void onEnter(GhostContext context);

    void onUpdateMovement(GhostContext ghost);

    void onCollisionWithPacman(GhostContext ghost);

    void onPowerPillConsumedByPacman(GhostContext ghost);

    void onPillTimerFinished(GhostContext ghost);

    void onRestoreTimerFinished(GhostContext ghost);

    void onRestorePointReached(GhostContext ghost);
}
