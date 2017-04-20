package production;

import java.time.LocalTime;

// you can change constructors, add methods etc.
// but you cannot remove or change the signatures of initialize and notifyOn
public class MutablePartOfDay {
    public void initialize(LocalTime time) {
        //only called at beginning,
        //no checks, sets the time passed
        //does not notify anobody
    }

    public void notifyOn(LocalTime time) {
        //changes the current part of day or stays at the same
        //if change is made, notifies the weather server
        //if a part of day is skipped, raises an error passing current part and received time
    }
}

