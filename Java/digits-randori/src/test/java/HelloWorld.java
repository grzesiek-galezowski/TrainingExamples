import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

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

    private String[] exampleOutput = new String[]{
        ".-.",
        "|.|",
        ".-.",
        "|.|",
        ".-."};

    private String digitIndexes =
        ".A." +
            "F.B" +
            ".G." +
            "E.C" +
            ".D.";

    @DataProvider(name = "testData")
    public static Object[][] testData() {
        return new Object[][]{
            test(
                forIndices(),
                expect(
                    "...",
                    "...",
                    "...",
                    "...",
                    "...")
            ),
            /*test(
                forIndices(
                    'A', 'B'
                ),
                expect(
                    ".-.",
                    "..|",
                    "...",
                    "...",
                    "...")
            ),
            */


        };
    }

    @Test(dataProvider = "testData")
    public void shouldXXXXXXXXXXXXXXX(Character[] input, String[] expected) {
        assertThat(input).isEqualTo(expected);

        //DigitalDisplay display = mock(DigitalDisplay.class);

        //verify(display).put("a", "b", "d");
    }

























    private static Object[] test(Character[] characters, String... strings) {
        return new Object[]{
            characters,
            strings,
        };
    }

    private static String[] expect(String... strings) {
        return strings;
    }

    private static Character[] forIndices(Character... chars) {
        return chars;
    }

}
