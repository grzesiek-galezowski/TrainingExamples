namespace KataTrainReservationTddEbook.Request;

public class ReservationRequestDto
{
  public readonly string TrainId;
  public readonly uint SeatCount;

  public ReservationRequestDto(string trainId, uint seatCount)
  {
    TrainId = trainId;
    SeatCount = seatCount;
  }
}