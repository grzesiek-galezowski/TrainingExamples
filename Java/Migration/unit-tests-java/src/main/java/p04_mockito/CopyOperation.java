package p04_mockito;

public class CopyOperation {
  public void applyTo(DataSource source, DataDestination destination) {
    Data data = source.retrieveData();
    destination.save(data);
  }
}
