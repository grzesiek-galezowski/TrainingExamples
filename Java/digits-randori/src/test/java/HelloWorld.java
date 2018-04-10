import org.junit.Test;
import thirdparty.DigitalDisplay;

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

  private String[] exampleOutput  = new String[] {
      ".-.",
      "|.|",
      ".-.",
      "|.|",
      ".-."};

  private String digitIndexes  =
      ".A." +
      "F.B" +
      ".G." +
      "E.C" +
      ".D.";

  @Test
  public void shouldXXXXXXXXXXXXXXX() {
    DigitalDisplay display = mock(DigitalDisplay.class);

    display.put("a", "b", "a");

    verify(display).put("a", "b", "d"); //should fail
  }
}
