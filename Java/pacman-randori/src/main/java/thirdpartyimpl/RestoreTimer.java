package thirdpartyimpl;

import main.Ghost;
import thirdparty.InGameTimer;

/**
 * Created by astral on 11.11.2015.
 */
public class RestoreTimer implements InGameTimer {
    private Ghost g;
    private boolean isRunning = false;

    @Override
    public void start() {
        isRunning = true;
        //some time after:
        g.onRestoreTimerFinished();
        isRunning = false;
        return isRunning;
    }

    public void reportExpiryTo(Ghost g) {
        this.g = g;
    }

}
