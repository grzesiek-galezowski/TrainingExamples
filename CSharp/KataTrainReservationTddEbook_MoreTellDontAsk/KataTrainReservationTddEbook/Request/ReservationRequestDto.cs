namespace KataTrainReservationTddEbook.Request;

public sealed record ReservationRequestDto(string TrainId, uint SeatCount)
{
  public readonly string TrainId = TrainId;
  public readonly uint SeatCount = SeatCount;
}