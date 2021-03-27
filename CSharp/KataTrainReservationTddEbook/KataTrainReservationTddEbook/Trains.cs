namespace KataTrainReservationTddEbook
{
  public interface Trains
  {
    Train RetrieveBy(TrainId trainId);
    void Update(Train train);
  }
}