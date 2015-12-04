package states;

import interfaces.GhostContext;
import interfaces.GhostState;
import interfaces.GhostStates;
import thirdparty.InGameTimer;
import thirdparty.GhostAppearance;
import thirdparty.Pacman;

/**
 * Created by astral on 11.11.2015.
 */
public class Chasing implements GhostState {

    private GhostStates states;
    private GhostAppearance appearance;
    private Pacman pacman;

    public Chasing(GhostStates states, GhostAppearance appearance, Pacman pacman) {
        this.states = states;
        this.appearance = appearance;
        this.pacman = pacman;
    }

    @Override
    public void onEnter(GhostContext context) {
        appearance.red();
    }

    @Override
    public void onUpdateMovement(GhostContext ghost) {
        pacman.moveTowards();
    }

    @Override
    public void onCollisionWithPacman(GhostContext ghost) {
        pacman.die();
    }

    @Override
    public void onPowerPillConsumedByPacman(GhostContext context) {
        context.changeStateTo(states.runningAway());
    }

    @Override
    public void onPillTimerFinished(GhostContext ghost) {
        throw new RuntimeException("impossible!");
    }

    @Override
    public void onRestoreTimerFinished(GhostContext ghost) {
        throw new RuntimeException("impossible!");
    }

    @Override
    public void onRestorePointReached(GhostContext ghost) {
        throw new RuntimeException("Imporssible");
    }
}
