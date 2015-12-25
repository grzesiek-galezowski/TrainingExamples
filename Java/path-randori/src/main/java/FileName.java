import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Optional;

/**
 * Created by ftw637 on 12/18/2015.
 */
public class FileName {

  private final Path path;
  private FileExtension fileExtension;

  public FileName(String arg) {
    this.path = Paths.get(arg);
    try {
      fileExtension = FileExtension.value(arg.split(".")[1]);
    } catch (Exception e) {
    }
  }

  public static FileName value(String arg) {
    return new FileName(arg);
  }

  @Override
  public String toString() {
    return path.toString();
  }

  public Optional<FileExtension> extension() {
    if(fileExtension == null) {
      return Optional.empty();
    } else {
      return Optional.of(fileExtension);
    }
  }
}
