import junitparams.JUnitParamsRunner;
import junitparams.Parameters;
import org.junit.Test;
import org.junit.runner.RunWith;

import java.util.Optional;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

/**
 * Created by ftw637 on 12/18/2015.
 */
@RunWith(JUnitParamsRunner.class)
public class FileNameTest {
  @Test
  @Parameters({"fileName, fileName"})
  public void shouldReturnFileName(String arg, String result){
    FileName value = FileName.value(arg);
    assertEquals(result, value.toString());
  }

  @Test
  public void shouldReturnOptionalExtension(){
    FileName value = FileName.value("file");
    Optional<FileExtension> extension = value.extension();
    FileExtension x = extension.orElse(null);
    assertFalse(extension.isPresent());
    assertEquals(Optional.empty(), extension);
  }
}
/*
public class FileName {

  FileName(String path);

  //not null
  //not empty
  //consists solely of file name (i.e. not "lol\lol.txt"
  public static FileName value(String path);
  @Override
  public String toString();
  @Override
  public boolean equals(Object obj);
  @Override
  public int hashCode();
  public Optional<FileExtension> extension();
  public FileName changeExtensionTo(FileExtension value);
}

public class FileExtension {

  FileExtension(String extension);
  //must not be null
  //must be empty
  //must consist solely of extension
  public static FileExtension value(String extensionString);

  @Override public String toString();
  @Override public boolean equals(Object obj);
  @Override public int hashCode();

}
*/