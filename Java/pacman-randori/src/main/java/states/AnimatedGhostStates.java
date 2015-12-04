package states;

import interfaces.GhostState;
import interfaces.GhostStates;
import thirdparty.InGameTimer;
import thirdparty.GhostAppearance;
import thirdparty.Pacman;
import thirdparty.Location;

/**
 * Created by astral on 11.11.2015.
 */
public class AnimatedGhostStates implements GhostStates {
    private GhostAppearance appearance;
    private Pacman pacman;
    private Location location;
    private InGameTimer restoreTimer;

    public AnimatedGhostStates(GhostAppearance appearance, Pacman pacman, Location location, InGameTimer restoreTimer) {
        this.appearance = appearance;
        this.pacman = pacman;
        this.location = location;
        this.restoreTimer = restoreTimer;
    }

    @Override
    public GhostState chasing() {
        return new Chasing(this, appearance, pacman);
    }

    @Override
    public GhostState runningAway() {
        return new RunningAway(pacman, appearance, this);
    }

    @Override
    public GhostState seekingRestore() {
        return new SeekingRestore(appearance, location, this);
    }

    @Override
    public GhostState restoring() {
        return new Restoring(restoreTimer, this);
    }
}
