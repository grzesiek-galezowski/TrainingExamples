namespace MockNoMock.MarsRide;

public class MarsRover
{
  private readonly Directions _currentDirection;
  private Position _position;
  private readonly IBoard _board;

  public MarsRover(Directions initialDirection, IBoard board, Position initialPosition)
  {
    _currentDirection = initialDirection;
    _board = board;
    _position = initialPosition;
  }

  public void MoveForward()
  {
    _position = _position.OneStepTowards(_currentDirection);
    _board.GoToPosition(_position);
  }
}