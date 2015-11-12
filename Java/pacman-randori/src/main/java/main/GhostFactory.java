package main;

import interfaces.GhostStates;
import main.GhostStateMachine;

/**
 * Created by astral on 12.11.2015.
 */
public class GhostFactory {
    public static GhostStateMachine createGhost(GhostStates states) {
        return new GhostStateMachine(states.chasing(), states);
    }
}
