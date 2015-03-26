import java.io.Closeable;
import java.io.IOException;
import java.util.Arrays;

/**
 * Created by astral on 24.03.15.
 */
public class SqlRepository implements Closeable {
  public void Dispose() {

  }

  public Iterable<Employee> CurrentEmployees() {
    return Arrays.asList(new Employee(), new Employee(), new Employee());
  }

  @Override
  public void close() throws IOException {
    System.out.println("Disposing");
  }
}