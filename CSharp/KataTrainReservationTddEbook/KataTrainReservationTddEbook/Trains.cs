namespace KataTrainReservationTddEbook;

public interface ITrains
{
  ITrain RetrieveBy(TrainId trainId);
  void Update(ITrain train);
}