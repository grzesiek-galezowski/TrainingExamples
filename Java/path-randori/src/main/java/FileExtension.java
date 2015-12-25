import java.util.Objects;

/**
 * Created by ftw637 on 12/18/2015.
 */
public class FileExtension {
  private final String extension;

  private FileExtension(String ext) throws Exception {
    validate(ext);
    extension = ext;
  }

  private void validate(String ext) throws Exception {
    if(ext == null || ext.isEmpty() || ext.contains(".")) {
      throw new Exception("Extension invalid");
    }
  }

  public static FileExtension value(String ext) throws Exception {
    return new FileExtension(ext);
  }

  @Override
  public String toString() {
    return  extension ;
  }


  @Override
  public boolean equals(Object o) {
    if (this == o) return true;
    if (o == null || getClass() != o.getClass()) return false;

    FileExtension that = (FileExtension) o;

    return extension.equals(that.extension);

  }

  @Override
  public int hashCode() {
    return extension.hashCode();
  }
}
