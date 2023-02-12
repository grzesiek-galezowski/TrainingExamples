namespace KataTrainReservationTddEbook.Response;

public class TicketDto
{
  public readonly string Coach;
  public readonly int SeatNumber;

  public TicketDto(string coach, int seatNumber)
  {
    Coach = coach;
    SeatNumber = seatNumber;
  }
}