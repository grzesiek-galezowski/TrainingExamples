namespace KataTrainReservationTddEbook
{
  public class TicketDto
  {
    public readonly string coach;
    public readonly int seatNumber;

    public TicketDto(string coach, int seatNumber)
    {
      this.coach = coach;
      this.seatNumber = seatNumber;
    }
  }
}