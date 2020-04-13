namespace KataTrainReservationTddEbook
{
  public interface Trains
  {
    Train RetrieveBy(string trainId);
    void Update(Train train);
  }
}