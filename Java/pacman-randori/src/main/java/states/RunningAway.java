package states;

import interfaces.GhostContext;
import interfaces.GhostState;
import interfaces.GhostStates;
import thirdparty.InGameTimer;
import thirdparty.GhostAppearance;
import thirdparty.Pacman;

public class RunningAway implements GhostState {
    private Pacman pacman;
    private GhostAppearance appearance;
    private GhostStates states;

    public RunningAway(Pacman pacman, GhostAppearance appearance, GhostStates states) {
        this.pacman = pacman;
        this.appearance = appearance;
        this.states = states;
    }

    @Override
    public void onEnter(GhostContext context) {
        appearance.blue();
    }

    @Override
    public void onUpdateMovement(GhostContext ghost) {
        pacman.moveAwayFrom();
    }

    @Override
    public void onCollisionWithPacman(GhostContext ghost) {
        ghost.changeStateTo(states.seekingRestore());
    }

    @Override
    public void onPowerPillConsumedByPacman(GhostContext ghost) {

    }

    @Override
    public void onPillTimerFinished(GhostContext context) {
        context.changeStateTo(states.chasing());
    }

    @Override
    public void onRestoreTimerFinished(GhostContext ghost) {
        throw new RuntimeException("cannot happen");
    }

    @Override
    public void onRestorePointReached(GhostContext ghost) {
        throw new RuntimeException("Imporssible");
    }
}
