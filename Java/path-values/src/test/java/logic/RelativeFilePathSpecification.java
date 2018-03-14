package logic;

import autofixture.publicinterface.Any;
import lombok.val;
import org.testng.annotations.Test;

import java.nio.file.Paths;

import static com.github.grzesiek_galezowski.test_environment.XAssertJConditions.corectlyImplementedEquality;
import static logic.InlineGenerators.fileName;
import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

public class RelativeFilePathSpecification {
    @Test
    public void shouldNotAllowToBeCreatedWithNullValue() {
        assertThatThrownBy(() -> RelativeFilePath.from(null))
            .isInstanceOf(NullPointerException.class);

    }

    @Test
    public void shouldThrowExceptionWhenTryingToCreateInstanceWithRootedPath() {
        assertThatThrownBy(() -> RelativeFilePath.from("C:\\Dir"))
            .isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void shouldThrowArgumentExceptionWhenTryingToCreateInstanceWithEmptyValue() {
        assertThatThrownBy(() -> RelativeFilePath.from(""))
            .isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void shouldImplementEquality() {
        assertThat(RelativeFilePath.class).has(corectlyImplementedEquality());
    }

    @Test
    public void shouldAllowAccessingDirectoryOfThePathWhenSuchDirectoryExists() {
        //GIVEN
        val dirPath = Any.instanceOf(RelativeDirectoryPath.class);
        val fileName = Any.anonymous(fileName());
        RelativeFilePath filePath = dirPath.with(fileName);

        //WHEN
        val dirObtainedFromPath = filePath.parent();


        //THEN
        assertThat(dirObtainedFromPath.get()).isEqualTo(dirPath);
    }

    @Test
    public void shouldReturnNothingWhenAskingForDirectoryOfThePathAndSuchDirectoryDoesNotExist() {
        //GIVEN
        RelativeFilePath filePath = RelativeFilePath.from("file.txt");

        //WHEN
        val dirObtainedFromPath = filePath.parent();

        //THEN
        assertThat(dirObtainedFromPath).isEmpty();
    }

    @Test
    public void shouldAllowAccessingFileNameOfThePath() {
        //GIVEN
        val dirPath = Any.instanceOf(RelativeDirectoryPath.class);
        val fileName = Any.anonymous(fileName());
        RelativeFilePath filePath = dirPath.with(fileName);

        //WHEN
        FileName fileNameObtainedFromPath = filePath.fileName();

        //THEN
        assertThat(fileNameObtainedFromPath).isEqualTo(fileName);
    }

    @Test
    public void shouldBeConvertibleToPath() {
        //GIVEN
        String inputString = "lolek\\lol.txt";
        val pathWithFilename = RelativeFilePath.from(inputString);

        //WHEN
        val fileInfo = pathWithFilename.toJavaPath();

        //THEN
        assertThat(fileInfo).isEqualTo(Paths.get(inputString));
    }


    /*
    @Test
    public void shouldBeConvertibleToAnyPathWithFileName()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<RelativeFilePath>();

      //WHEN
      AnyFilePath anyFilePath = pathWithFileName.AsAnyFilePath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyFilePath.ToString());
    }

    @Test
    public void shouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<RelativeFilePath>();

      //WHEN
      AnyPath anyPathWithFileName = pathWithFileName.AsAnyPath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyPathWithFileName.ToString());
    }

    [Theory,
     InlineData(@"Dir\Subdir\fileName.txt", ".txt", true),
     InlineData(@"Dir\Subdir\fileName.tx", ".txt", false),
     InlineData(@"Dir\Subdir\fileName", ".txt", false),
    ]
    public void shouldBeAbleToRecognizeWhetherItHasCertainExtension(string path, string extension, bool expectedResult)
    {
      //GIVEN
      var pathWithFileName = RelativeFilePath.Value(path);
      var extensionValue = FileExtension.Value(extension);

      //WHEN
      var hasExtension = pathWithFileName.Has(extensionValue);

      //THEN
      Assert.Equal(expectedResult, hasExtension);
    }

    @Test
    public void shouldAllowChangingExtension()
    {
      //GIVEN
      var filePath = RelativeFilePath.Value(@"Dir\subdir\file.txt");

      //WHEN
      RelativeFilePath pathWithNewExtension = filePath.ChangeExtensionTo(FileExtension.Value(".doc"));

      //THEN
      Assert.Equal(@"Dir\subdir\file.doc", pathWithNewExtension.ToString());

    }

    @Test
    public void shouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<RelativeFilePath>();
      var path2 = Any.Instance<RelativeFilePath>();
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