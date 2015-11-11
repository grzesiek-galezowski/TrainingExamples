package interfaces;

import interfaces.GhostState;

/**
 * Created by astral on 11.11.2015.
 */
public interface GhostStates {
    GhostState chasing();

    GhostState runningAway();

    GhostState seekingRestore();

    GhostState restoring();
}
