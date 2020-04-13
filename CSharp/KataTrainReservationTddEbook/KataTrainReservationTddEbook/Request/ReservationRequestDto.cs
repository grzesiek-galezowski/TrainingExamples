namespace KataTrainReservationTddEbook
{
  public class ReservationRequestDto
  {
    public readonly string trainId;
    public readonly uint seatCount;

    public ReservationRequestDto(string trainId, uint seatCount)
    {
      this.trainId = trainId;
      this.seatCount = seatCount;
    }
  }
}