namespace KataTrainReservationTddEbook;

public interface IFleet
{
  ITrain RetrieveBy(TrainId trainId);
  void Update(ITrain train);
}