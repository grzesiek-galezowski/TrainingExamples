package _09_mock_objects;

public class CopyOperation {
  public void applyTo(DataSource source, DataDestination destination) {
    Data data = source.retrieveData();
    destination.save(data);
  }
}
