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
    public void onEnter() {
        appearance.transparent();
    }

    @Override
    public void onUpdateMovement() {
        location.moveTowards();
    }

    @Override
    public void onCollisionWithPacman(GhostContext context) {
        //do nothing
    }

    @Override
    public void onPowerPillConsumedByPacman(GhostContext context) {

    }

    @Override
    public void onPillTimerFinished(GhostContext context) {
        //nothing
    }

    @Override
    public void onRestoreTimerFinished(GhostContext context) {
        throw new RuntimeException("impossible");
    }

    @Override
    public void onRestorePointReached(GhostContext context) {
        context.changeStateTo(this.states.restoring());
    }
}
