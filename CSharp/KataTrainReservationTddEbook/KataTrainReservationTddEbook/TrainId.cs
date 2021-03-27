using System;

namespace KataTrainReservationTddEbook
{
  public class TrainId
  {
    private readonly string _trainId;

    public TrainId(string trainId)
    {
      _trainId = trainId;
    }

    public override bool Equals(object obj)
    {
      return _trainId == ((TrainId) obj)._trainId;
    }
  }
}