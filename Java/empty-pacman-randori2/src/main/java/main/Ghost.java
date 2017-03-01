package main;

/**
 * Created by astral on 12.11.2015.
 */
public interface Ghost {
    void onUpdateMovement();

    void onCollisionWithPacman();

    void onPowerPillConsumedByPacman();

    void onPillEffectExpired();

    void onRestoreTimerFinished();

    void onRestorePointReached();
}
