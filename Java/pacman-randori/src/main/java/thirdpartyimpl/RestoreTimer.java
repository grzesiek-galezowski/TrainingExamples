package thirdpartyimpl;

import other.Ghost;
import thirdparty.InGameTimer;

/**
 * Created by astral on 11.11.2015.
 */
public class RestoreTimer implements InGameTimer {
    private Ghost g;

    @Override
    public void restart() {
        g.onRestoreTimerFinished();
    }

    public void reportExpiryTo(Ghost g) {

        this.g = g;
    }
}
