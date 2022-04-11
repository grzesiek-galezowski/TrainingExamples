namespace MockNoMock.MarsRide;

public sealed record Position(int X, int Y)
{
  //incomplete implementation
  public Position OneStepTowards(Directions currentDirection)
  {
    if (currentDirection == Directions.North)
    {
      return this with { Y = Y + 1 };
    }
    else
    {
      return this with { Y = Y - 1 };
    }
  }
}