package com.github.galezowski.grzegorz.code;

import java.util.function.Function;

public enum Direction {
    NORTH,
    SOUTH,
    WEST,
    EAST;

    public static <T> T when(
        final T instance,
        final Function<T, T> whenFacingNorth,
        final Function<T, T> whenFacingSouth,
        final Function<T, T> whenFacingEast,
        final Function<T, T> whenFacingWest,
        final Direction direction) {
        switch(direction) {
            case NORTH:
                return whenFacingNorth.apply(instance);
            case SOUTH:
                return whenFacingSouth.apply(instance);
            case WEST:
                return whenFacingWest.apply(instance);
            case EAST:
                return whenFacingEast.apply(instance);
        }
        throw new RuntimeException("trolololo");
    }
}
