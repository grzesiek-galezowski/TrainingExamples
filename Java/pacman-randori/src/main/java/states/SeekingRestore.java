package states;

import interfaces.GhostContext;
import interfaces.GhostState;
import interfaces.GhostStates;
import thirdparty.GhostAppearance;
import thirdparty.Location;

/**
 * Created by astral on 11.11.2015.
 */
public class SeekingRestore implements GhostState {
    private GhostAppearance appearance;
    private Location location;
    private GhostStates states;

    public SeekingRestore(GhostAppearance appearance, Location location, GhostStates states) {
        this.appearance = appearance;
        this.location = location;
        this.states = states;
    }

    @Override
    public void onEnter(GhostContext context) {
        appearance.transparent();
    }

    @Override
    public void onUpdateMovement(GhostContext ghost) {
        location.moveTowards();
    }

    @Override
    public void onCollisionWithPacman(GhostContext ghost) {
        //do nothing
    }

    @Override
    public void onPowerPillConsumedByPacman(GhostContext ghost) {

    }

    @Override
    public void onPillTimerFinished(GhostContext ghost) {
        //nothing
    }

    @Override
    public void onRestoreTimerFinished(GhostContext ghost) {
        throw new RuntimeException("impossible");
    }

    @Override
    public void onRestorePointReached(GhostContext context) {
        context.changeStateTo(this.states.restoring());
    }
}
