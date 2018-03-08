package com.github.galezowski.grzegorz.code;

public class Point {
    public final int X;
    public final int Y;

    public Point(final int x, final int y) {
        X = x;
        Y = y;
    }

    public static Point toTheWest(final Point position) {
        //todo consider reusing Direction.when similar to turn()
        return new Point(position.X - 1, position.Y);
    }

    public static Point toTheEast(final Point position) {
        //todo consider reusing Direction.when similar to turn()
        return new Point(position.X + 1, position.Y);
    }

    public static Point toTheSouth(final Point position) {
        //todo consider reusing Direction.when similar to turn()
        return new Point(position.X, position.Y - 1);
    }

    public static Point toTheNorth(final Point position) {
        //todo consider reusing Direction.when similar to turn()
        return new Point(position.X, position.Y + 1);
    }
}
