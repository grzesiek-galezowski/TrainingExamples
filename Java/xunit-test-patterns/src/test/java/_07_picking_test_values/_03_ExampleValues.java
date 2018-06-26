package _07_picking_test_values;

import lombok.val;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class _03_ExampleValues {

    @DataProvider
    public static Object[][] shouldCorrectlyCheckNamesData() {
        return new Object[][]{
            {"aaaaa", false}, //must start with capital letter
            {"A"    , false}, //cannot be a one-letter name
            {"Aa"   , true }, //valid input
            {"0"    , false}, //cannot have digits
            {"&"    , false}  //cannot have special chars
        };
    }

    @Test(dataProvider = "shouldCorrectlyCheckNamesData")
    public void shouldCorrectlyCheckNames(
        String nameString,
        boolean expectedResult) {

        //GIVEN
        val userNameCheck = new UserNameCheck();
        //WHEN
        val isUserNameValid = userNameCheck.applyTo(nameString);
        //THEN
        assertThat(isUserNameValid).isEqualTo(expectedResult);
    }

}
