package other;

/**
 * Created by ftw637 on 11/25/2015.
 */
public interface Ghost {
    void onUpdateMovement();

    void onCollisionWithPacman();

    void onPowerPillConsumedByPacman();

    void onPillEffectExpired();

    void onRestoreTimerFinished();

    void onRestorePointReached();
}
