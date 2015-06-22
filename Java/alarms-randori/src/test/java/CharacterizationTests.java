import Main.CompositionRoot;
import org.junit.Assert;
import org.junit.Test;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.fail;

public class CharacterizationTests {

  @Test
  public void should() {
    //GIVEN
    ByteArrayOutputStream stream = new ByteArrayOutputStream();
    System.setOut(new PrintStream(stream));

    CompositionRoot.main();
    String[] str = stream.toString().split("\r\n");

    assertEquals("Calling 11-222-1121\r\n" +
        "Stopped playing\r\n" +
        "Recalling 11-222-1121\r\n" +
        "========DUMP=========\r\n" +
        "{ Both: \r\n" +
        "{ Timed Alarm active when: \r\n" +
        "it's night\r\n" +
        "it's weekend\r\n" +
        "When triggered : \r\n" +
        "{ Playing loud sound }\r\n" +
        " }\r\n" +
        "{ Calls: 11-222-1121 }\r\n" +
        "} " + "\r\n", stream.toString());
  }
}
