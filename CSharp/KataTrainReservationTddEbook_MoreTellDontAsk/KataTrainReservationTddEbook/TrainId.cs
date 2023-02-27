namespace KataTrainReservationTddEbook;

public sealed record TrainId
{
  private readonly string _trainId;

  public TrainId(string trainId)
  {
    _trainId = trainId;
  }
}