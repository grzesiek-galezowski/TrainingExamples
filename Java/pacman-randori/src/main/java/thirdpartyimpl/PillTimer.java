package thirdpartyimpl;

import main.Ghost;
import thirdparty.InGameTimer;

/**
 * Created by astral on 11.11.2015.
 */
public class PillTimer implements InGameTimer {
    private Ghost g;
    private boolean isRunning = false;

    @Override
    public void restart() {
        isRunning = true;
        //some time after:
        g.onPillTimerFinished();
        isRunning = false;
    }

    @Override
    public boolean isRunning() {
        return isRunning;
    }

    public void reportExpiryTo(Ghost g) {
        this.g = g;
    }
}
