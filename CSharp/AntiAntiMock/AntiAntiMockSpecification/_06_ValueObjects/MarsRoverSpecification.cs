using MockNoMock.MarsRide;

namespace MockNoMockSpecification._06_ValueObjects;

internal class MarsRoverSpecification
{
  [TestCase(Directions.North, 1)]
  [TestCase(Directions.South, -1)]
  public void ShouldBeAbleToMoveTowardsItsInitialDirection(Directions initialDirection, int yIncrement)
  {
    //GIVEN
    var board = Substitute.For<IBoard>();
    var initialPosition = new Position(0,0);
    var marsRover = new MarsRover(initialDirection, board, initialPosition);
    
    //WHEN
    marsRover.MoveForward();

    //THEN
    board.Received(1).GoToPosition(new Position(0, yIncrement));
  }

  //TODO: other tests
}