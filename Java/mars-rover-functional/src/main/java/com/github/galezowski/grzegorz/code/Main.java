package com.github.galezowski.grzegorz.code;

public class Main {

    public static void main(String[] args) {
        //todo immutable array
        final Rover rover = new Rover(new Point(0,0), Direction.NORTH);
        final RoverCommands[] commands = new RoverCommands[] {
            RoverCommands.F, //0,1,N
            RoverCommands.L, //0,1,W
            RoverCommands.B, //1,1,F
            RoverCommands.R, //1,1,N
        };

        Rover rover2 = rover.apply(commands);


    }
}
