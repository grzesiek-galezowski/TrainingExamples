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
    public void onEnter() {
        appearance.blue();
    }

    @Override
    public void onUpdateMovement() {
        pacman.moveAwayFrom();
    }

    @Override
    public void onCollisionWithPacman(GhostContext context) {
        context.changeStateTo(states.seekingRestore());
    }

    @Override
    public void onPowerPillConsumedByPacman(GhostContext context) {

    }

    @Override
    public void onPillTimerFinished(GhostContext context) {
        context.changeStateTo(states.chasing());
    }

    @Override
    public void onRestoreTimerFinished(GhostContext context) {
        throw new RuntimeException("cannot happen");
    }

    @Override
    public void onRestorePointReached(GhostContext context) {
        throw new RuntimeException("Imporssible");
    }
}
