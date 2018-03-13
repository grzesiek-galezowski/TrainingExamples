package logic;

import autofixture.publicinterface.Any;
import lombok.val;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import java.nio.file.Paths;

import static com.github.grzesiek_galezowski.test_environment.XAssertJConditions.corectlyImplementedEquality;
import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

public class DirectoryNameSpecification {

    @Test
    public void shouldBehaveLikeValue() {
        assertThat(DirectoryName.class).has(corectlyImplementedEquality());
    }

    @DataProvider(name = "invalid values")
    public static Object[][] invalidValues() {
        return new Object[][]{
            {null, NullPointerException.class},
            {"", IllegalArgumentException.class},
            {"?", IllegalArgumentException.class},
            {"|", IllegalArgumentException.class},
            {"\"", IllegalArgumentException.class},
            {"C:\\a", IllegalArgumentException.class},
            {"\\\\\\\\\\\\\\\\\\\\/\\/", IllegalArgumentException.class}
        };
    }

    @Test(dataProvider = "invalid values")
    public void shouldThrowExceptionWhenCreatedWithInvalidValue(
        String invalidName,
        Class exceptionClass
    ) {
        assertThatThrownBy(() ->
            DirectoryName.from(invalidName)
        ).isInstanceOf(exceptionClass);
    }

    @Test
    public void shouldBeConvertibleToString() {
        //GIVEN
        val sourceString = Any.alphaString();
        val dirName = DirectoryName.from(sourceString);

        //WHEN
        val asString = dirName.toString();

        //THEN
        assertThat(asString).isEqualTo(sourceString);
    }

    @Test
    public void shouldBeConvertibleToPath() {
        //GIVEN
        val sourceString = Any.alphaString();
        val dirName = DirectoryName.from(sourceString);

        //WHEN
        val asPath = dirName.toJavaPath();

        //THEN
        assertThat(asPath).isEqualTo(Paths.get(sourceString));
    }
    /*

    @Test
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<DirectoryName>();
      var path2 = Any.Instance<DirectoryName>();
      var fileSystemComparisonRules = Substitute.For<FileSystemComparisonRules>();
      var comparisonResult = Any.Boolean();

      fileSystemComparisonRules
        .ArePathStringsEqual(path1.ToString(), path2.ToString())
        .Returns(comparisonResult);


      //WHEN
      var equality = path1.ShallowEquals(path2, fileSystemComparisonRules);

      //THEN
      XAssert.Equal(comparisonResult, equality);
    }
     */
}