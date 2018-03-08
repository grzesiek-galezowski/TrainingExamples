package com.github.galezowski.grzegorz.code;

import java.util.Arrays;

import static com.github.galezowski.grzegorz.code.Direction.EAST;
import static com.github.galezowski.grzegorz.code.Direction.NORTH;
import static com.github.galezowski.grzegorz.code.Direction.SOUTH;
import static com.github.galezowski.grzegorz.code.Direction.WEST;
import static com.github.galezowski.grzegorz.code.Direction.when;
import static com.github.galezowski.grzegorz.code.Point.toTheEast;
import static com.github.galezowski.grzegorz.code.Point.toTheNorth;
import static com.github.galezowski.grzegorz.code.Point.toTheSouth;
import static com.github.galezowski.grzegorz.code.Point.toTheWest;


public class Rover {
    public final Point position;
    public final Direction direction;

    public Rover(final Point position, final Direction direction) {
        this.position = position;
        this.direction = direction;
    }

    public Rover apply(final RoverCommands[] commands) {

        return Arrays.stream(commands).reduce(this, (rover, command) ->
                RoverCommands.interpret(rover, command,
                    orderedForward  -> when(orderedForward,
                        facingNorth -> move(toTheNorth(facingNorth.position), facingNorth),
                        facingSouth -> move(toTheSouth(facingSouth.position), facingSouth),
                        facingEast  -> move(toTheEast(facingEast.position), facingEast),
                        facingWest  -> move(toTheWest(facingWest.position), facingWest),
                        orderedForward.direction),
                    orderedBackward -> when(orderedBackward,
                        facingNorth -> move(toTheSouth(facingNorth.position), facingNorth),
                        facingSouth -> move(toTheNorth(facingSouth.position), facingSouth),
                        facingEast  -> move(toTheWest(facingEast.position), facingEast),
                        facingWest  -> move(toTheEast(facingWest.position), facingWest),
                        orderedBackward.direction),
                    orderedToTurnLeft -> when(orderedToTurnLeft,
                        facingNorth -> turn(WEST, facingNorth),
                        facingSouth -> turn(EAST, facingSouth),
                        facingEast  -> turn(NORTH, facingEast),
                        facingWest  -> turn(SOUTH, facingWest),
                        orderedToTurnLeft.direction),
                    orderedToTurnRight -> when(orderedToTurnRight,
                        facingNorth -> turn(EAST, facingNorth),
                        facingSouth -> turn(WEST, facingSouth),
                        facingEast  -> turn(SOUTH, facingEast),
                        facingWest  -> turn(NORTH, facingWest),
                        orderedToTurnRight.direction)),
            (rover, rover2) -> rover);
    }

    private static Rover turn(final Direction direction, final Rover rover) {
        return new Rover(rover.position, direction);
    }

    private static Rover move(final Point point, final Rover r) {
        return new Rover(point, r.direction);
    }


}
