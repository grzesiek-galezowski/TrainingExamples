package main;

import interfaces.GhostStates;
import other.AnimatedGhost;

/**
 * Created by astral on 12.11.2015.
 */
public class GhostFactory {
    public static Ghost createGhost(GhostStates states) {
        return new AnimatedGhost(states.chasing());
    }
}
