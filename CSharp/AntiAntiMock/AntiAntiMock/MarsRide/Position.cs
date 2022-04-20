namespace MockNoMock.MarsRide;

//Other examples:
//- string
//- TimeSpan
//- DateTime
//- Guid
public sealed record Position(int X, int Y)
{
  //incomplete implementation
  public Position OneStepTowards(Directions currentDirection)
  {
    if (currentDirection == Directions.North)
    {
      return new Position(X, Y + 1);
    }
    else
    {
      return new Position(X, Y - 1);
    }
  }
}