import org.junit.Test;
import thirdparty.DigitalDisplay;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.fail;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.verify;

//task
//write code that uses DigitalDisplay (i.e. DigitalDisplayDriver or sth. )
// for 7 segment display:
// ".-."
// "|.|"
// ".-."
// "|-|"
// ".-."
// and allow passing indexes (see DigitalDisplayDriver class)
// next possible exercises:
//1. Add support for more rows
//2. display with memory - you can "add" to lightened and "put out" some additional lights
//3. Add support to cells that are toggleable, e.g. first time show "|" or "-", second time they show "*", third time they show "." (dark)
//4. change the code so that no return values are used


public class HelloWorld {

  private String exampleOutput  =
      ".-." +
      "|.|" +
      ".-." +
      "|.|" +
      ".-.";

  private String digitIndexes  =
      ".0." +
      "1.2" +
      ".3." +
      "4.5" +
      ".6.";

  @Test
  public void shouldXXXXXXXXXXXXXXX() {
    DigitalDisplay display = mock(DigitalDisplay.class);

    display.put("a", "b", "c");

    verify(display).put("a", "b", "d"); //should fail
  }
}
