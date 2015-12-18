import com.sun.xml.internal.bind.annotation.OverrideAnnotationOf;
import junitparams.JUnitParamsRunner;
import junitparams.Parameters;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;

import java.nio.file.FileSystem;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Optional;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertThat;
import static org.junit.Assert.assertTrue;

/**
 * Created by ftw637 on 12/18/2015.
 */
@RunWith(JUnitParamsRunner.class)
public class FileExtensionTest {
  @Test
  @Parameters({"txt, txt"})
  public void shouldReturnFileExtensionValue(String arg, String result) throws Exception {
    //Path p = Paths.get("C:\\lolek");
    /// assertTrue(p.isAbsolute());
    // assertEquals("lolek2", p.getFileName().toString());

    FileExtension ext = FileExtension.value(arg);
    assertEquals(result, ext.toString());
  }

  @Test(expected = Exception.class)
  public void shouldThrowAnExceptionWhenExtIsNulle() throws Exception {
    FileExtension ext = FileExtension.value(null);
  }

  @Test(expected = Exception.class)
  @Parameters({"", ".txt"})
  public void shouldThrowAnExceptionWhenInvalid(String arg) throws Exception {
    FileExtension ext = FileExtension.value(arg);
  }

  @Test
  @Parameters({"true,txt,txt","false,txt,mopwepjfueoiwyfh","false,txt,TXT"})
  public void shouldEqualBasedOnParams(boolean equals, String arg1, String arg2) throws Exception {
    FileExtension ext = FileExtension.value(arg1);
    FileExtension ext2 = FileExtension.value(arg2);
    Assert.assertTrue(equals == ext.equals(ext2));
    Assert.assertTrue(equals == ext2.equals(ext));
  }
}
//examples:
// absolute file path: C:\lolek\lolek.txt
// absolute directory path: C:\lolek\lolek.txt
// file name: lolek.txt
// file extension: txt
//use Path class (Paths.get())

/*
public class AbsoluteFilePath {

  AbsoluteFilePath(String path);

  // must be not null
  // must be not empty
  // must be absolute
  public static AbsoluteFilePath value(String path);
  public AbsoluteDirectoryPath parentDirectory()
  public Path asPath();
  public FileName fileName();
  public AbsoluteDirectoryPath root();
  @Override
  public String toString();
  @Override
  public boolean equals(Object obj);
  @Override
  public int hashCode();
}


public class AbsoluteDirectoryPath
{
  AbsoluteDirectoryPath(String path);

  //returns object when:
  //not empty
  //not null
  //is absolute
  public static AbsoluteDirectoryPath value(String path);

  public String toString();
  @Override public boolean equals(Object other);
  @Override public int hashCode();

  public Path asPath();
  public Optional<AbsoluteDirectoryPath> parentDirectory(); //nothing if path is root
  public AbsoluteDirectoryPath root();
}

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