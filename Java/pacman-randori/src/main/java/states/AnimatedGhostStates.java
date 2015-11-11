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
    private InGameTimer pillTimer;
    private Location location;
    private InGameTimer restoreTimer;

    public AnimatedGhostStates(GhostAppearance appearance, Pacman pacman, InGameTimer pillTimer, Location location, InGameTimer restoreTimer) {
        this.appearance = appearance;
        this.pacman = pacman;
        this.pillTimer = pillTimer;
        this.location = location;
        this.restoreTimer = restoreTimer;
    }

    @Override
    public GhostState chasing() {
        return new Chasing(this, appearance, pacman, pillTimer);
    }

    @Override
    public GhostState runningAway() {
        return new RunningAway(pacman, appearance, pillTimer, this);
    }

    @Override
    public GhostState seekingRestore() {
        return new SeekingRestore(appearance, location, pillTimer);
    }

    @Override
    public GhostState restoring() {
        return new Restoring(restoreTimer, pillTimer, this);
    }
}
