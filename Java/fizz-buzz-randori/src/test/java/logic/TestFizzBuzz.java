package logic;//Fizz Buzz
// int -> FB -> String
//

//konwersje int -> String:
//////////////////////////
//String.valueOf(num);
//Integer.toString(str)

//Triangulacja
//1 -> return "1";
//2 -> Integer.toString(n);
//3 -> n == 3 == 0

//Skróty:
//ALT+ENTER - quick fix
//CTRL+ALT+SHIFT+T - refactor this
//CTRL+ALT+L/P - poprzednie/następne
//CTRL+B - go to declaration
//CTRL+ALT+B - go to implementation

/*
    1 -> return "1";
    2 -> return String.valueOf(n);

    N  FizzBuzz  "N"
    N%3  FizzBuzz  "Fizz"
    N%5  FizzBuzz  "Buzz"
    N%3 && N%5  FizzBuzz  "FizzBuzz"
 */

//ALT+ENTER - quick fix
//SHIFT+F6 - rename
//CTRL+D - duplicate line

//CTRL+ALT+SHIFT+T - refactor this
//CTRL+SPACE

import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class TestFizzBuzz {

    @DataProvider
    public Object[][] params() {
        return new Object[][]{
            {1, "1"},
            {1, "2"},
            {1, "4"},
        };
    }

    //ALT + ENTER
    @Test(dataProvider = "params")
    public void testConvertsIntToStringAccordingToFizzBuzz(int input, String expected) {
        //GIVEN

        //WHEN

        //THEN
        assertThat(input).isEqualTo(expected);
    }


}