package com.github.galezowski.grzegorz.code;

import java.util.function.Function;

public enum RoverCommands {
    F,
    B,
    L,
    R;

    //todo implement front and back
    public static <T> T interpret(final T rover, final RoverCommands command,
                                  final Function<T, T> whenForward,
                                  final Function<T, T> whenBackward,
                                  final Function<T, T> whenLeft,
                                  final Function<T, T> whenRight) {

        switch(command) {
            case F:
                return whenForward.apply(rover);
            case B:
                return whenBackward.apply(rover);
            case L:
                return whenLeft.apply(rover);
            case R:
                return whenRight.apply(rover);
        }
        throw new RuntimeException("trolololo");
    }
}
