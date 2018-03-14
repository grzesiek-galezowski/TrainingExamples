package logic;

import lombok.val;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import java.nio.file.Paths;
import java.util.Optional;

import static com.github.grzesiek_galezowski.test_environment.XAssertJConditions.corectlyImplementedEquality;
import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

public class RelativeDirectoryPathSpecification {

    @Test
    public void shouldBehaveLikeValue() {
        assertThat(RelativeDirectoryPath.class)
            .has(corectlyImplementedEquality());
    }

    @Test
    public void shouldAllowAddingDirectoryNameToIt() {
        //GIVEN
        val relativeDir = RelativeDirectoryPath.from("lolek\\bolek");
        val dirName = DirectoryName.from("zenek");

        //WHEN
        RelativeDirectoryPath mergedPath = relativeDir.with(dirName);

        //THEN
        assertThat(mergedPath.toString())
            .isEqualTo("lolek\\bolek\\zenek");
    }

    @Test
    public void shouldAllowAddingFileNameToIt() {
        //GIVEN
        val relativeDir = RelativeDirectoryPath.from("lolek\\bolek");
        val fileName = FileName.from("zenek.txt");

        //WHEN
        RelativeFilePath mergedFilePath = relativeDir.with(fileName);

        //THEN
        assertThat(mergedFilePath.toString())
            .isEqualTo("lolek\\bolek\\zenek.txt");
    }

    @Test
    public void shouldAllowAddingRelativeDirectoryPathToIt() {
        //GIVEN
        val relativeDir1 = RelativeDirectoryPath.from("Dir1\\dir2");
        val relativeDir2 = RelativeDirectoryPath.from("dir3\\dir4");

        //WHEN
        RelativeDirectoryPath mergedPath = relativeDir1.with(relativeDir2);

        //THEN
        assertThat(mergedPath.toString()).isEqualTo("Dir1\\dir2\\dir3\\dir4");
    }

    @Test
    public void shouldAllowAddingRelativePathWithFileNameToIt() {
        //GIVEN
        val relativeDir1 = RelativeDirectoryPath.from("Dir1\\dir2");
        val relativePathWithFileName = RelativeFilePath.from("dir3\\dir4\\file.txt");

        //WHEN
        RelativeFilePath mergedFilePath = relativeDir1
            .with(relativePathWithFileName);

        //THEN
        assertThat(mergedFilePath.toString())
            .isEqualTo("Dir1\\dir2\\dir3\\dir4\\file.txt");
    }

    @Test
    public void shouldAllowGettingPathWithoutLastDirectory() {
        //GIVEN
        val relativePath = RelativeDirectoryPath.from("Directory\\Subdirectory\\Subsubdirectory");

        //WHEN
        Optional<RelativeDirectoryPath> pathWithoutLastDir
            = relativePath.parent();

        //THEN
        assertThat(pathWithoutLastDir.get())
            .isEqualTo(RelativeDirectoryPath.from("Directory\\Subdirectory"));

    }

    @Test
    public void shouldReturnNothingWhenGettingPathWithoutLastDirectoryButCurrentDirectoryIsTheOnlyLeft() {
        //GIVEN
        val relativePath = RelativeDirectoryPath.from("Directory");

        //WHEN
        Optional<RelativeDirectoryPath> pathWithoutLastDir
            = relativePath.parent();

        //THEN
        assertThat(pathWithoutLastDir).isEmpty();
    }

    @DataProvider(name = "invalid values")
    public static Object[][] invalidValues() {
        return new Object[][]{
            {null, NullPointerException.class},
            {"", IllegalArgumentException.class},
            {"?", IllegalArgumentException.class},
            {"|", IllegalArgumentException.class},
            {"\"", IllegalArgumentException.class},
            {"C:\\", IllegalArgumentException.class},
            {"\\\\\\\\\\\\\\\\\\\\/\\/", IllegalArgumentException.class}
        };
    }

    @Test(dataProvider = "invalid values")
    public void shouldThrowExceptionWhenCreatedWithInvalidValue(
        String invalidName,
        Class exceptionClass
    ) {
        assertThatThrownBy(() ->
            RelativeDirectoryPath.from(invalidName)
        ).isInstanceOf(exceptionClass);
    }

    @Test
    public void shouldBeConvertibleToDirectoryInfo() {
        //GIVEN
        String input = "Dir\\Subdir";
        val path = RelativeDirectoryPath.from(input);

        //WHEN
        val directoryInfo = path.toJavaPath();

        //THEN
        assertThat(directoryInfo).isEqualTo(Paths.get(input));
    }

    @DataProvider(name = "multiple segments")
    public static Object[][] multipleSegments() {
        return new Object[][]{
            {"Segment1\\Segment2\\", "Segment2"},
            {"Segment1", "Segment1"},
        };
    }

    @Test(dataProvider = "multiple segments")
    public void shouldAllowGettingTheNameOfCurrentDirectory(
        String fullPath, String expectedDirectoryName) {
        //GIVEN
        val directoryPath = RelativeDirectoryPath.from(fullPath);

        //WHEN
        DirectoryName dirName = directoryPath.directoryName();

        //THEN
        assertThat(dirName).isEqualTo(
            DirectoryName.from(expectedDirectoryName));
    }


    /*

    @Test
    public void shouldBeConvertibleToAnyDirectoryPath()
    {
      //GIVEN
      var dirPath = Any.Instance<RelativeDirectoryPath>();

      //WHEN
      AnyDirectoryPath anyDirectoryPath = dirPath.AsAnyDirectoryPath();

      //THEN
      Assert.Equal(dirPath.ToString(), anyDirectoryPath.ToString());
    }

    @Test
    public void shouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var directorypath = Any.Instance<RelativeDirectoryPath>();

      //WHEN
      AnyPath anyPathWithFileName = directorypath.AsAnyPath();

      //THEN
      Assert.Equal(directorypath.ToString(), anyPathWithFileName.ToString());
    }


    private static string FullNameFrom(RelativeDirectoryPath path)
    {
      return Path.Combine(new DirectoryInfo(".").FullName, path.ToString());
    }

    @Test
    public void shouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<RelativeDirectoryPath>();
      var path2 = Any.Instance<RelativeDirectoryPath>();
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
  }
     */

}