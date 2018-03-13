package logic;

import autofixture.publicinterface.Any;
import lombok.val;
import org.testng.annotations.Test;

import java.nio.file.Paths;

import static com.github.grzesiek_galezowski.test_environment.XAssertJConditions.corectlyImplementedEquality;
import static logic.InlineGenerators.fileName;
import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

public class AbsoluteFilePathSpecification {

    @Test
    public void shouldNotAllowToBeCreatedWithNullValue() {
        assertThatThrownBy(() -> AbsoluteFilePath.from(null))
            .isInstanceOf(NullPointerException.class);
    }

    @Test
    public void shouldThrowArgumentExceptionWhenTryingToCreateInstanceWithNotWellFormedUri() {
        assertThatThrownBy(() -> AbsoluteFilePath.from("C:\\?||\\|\\|\\"))
            .isInstanceOf(IllegalArgumentException.class);
    }
    @Test
    public void shouldThrowArgumentExceptionWhenTryingToCreateInstanceWithNotAbsolutePath() {
        assertThatThrownBy(() -> AbsoluteFilePath.from("lolek\\lolki2"))
            .isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void shouldThrowArgumentExceptionWhenTryingToCreateInstanceWithRootPath() {
        assertThatThrownBy(() -> AbsoluteFilePath.from("C:\\"))
            .isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void shouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString() {
        String inputPathString = "c:\\lolek\\lolki2.txt";
        AbsoluteFilePath path = AbsoluteFilePath
            .from(inputPathString);

        assertThat(path).isNotNull();
        assertThat(path.toString()).isEqualTo(inputPathString);
    }

    @Test
    public void shouldBehaveLikeValueObject() {
        assertThat(AbsoluteFilePath.class)
            .has(corectlyImplementedEquality());
    }

    @Test
    public void shouldReturnTheStringItWasCreatedWithWhenConvertedToString() {
        //GIVEN
        val initialValue = "C:\\Dir\\Subdir\\file.csproj";
        val path = AbsoluteFilePath.from(initialValue);

        //WHEN
        val convertedToString = path.toString();

        //THEN
        assertThat(convertedToString).isEqualTo(initialValue);
    }

    @Test
    public void shouldBeConvertibleToPath() {
        //GIVEN
        val initialValue = "C:\\Dir\\Subdir\\file.csproj";
        val path = AbsoluteFilePath.from(initialValue);

        //WHEN
        val convertedToPath = path.toJavaPath();

        //THEN
        assertThat(convertedToPath).isEqualTo(Paths.get(initialValue));
    }

    //todo add check to isAbsolute

    @Test
    public void shouldAllowAccessingDirectoryOfThePath() {
        //GIVEN
        val dirPath = Any.instanceOf(AbsoluteDirectoryPath.class);
        val fileName = Any.anonymous(fileName());
        AbsoluteFilePath absoluteFilePath = dirPath.with(fileName);

        //WHEN
        val dirObtainedFromPath = absoluteFilePath.parent();

        //THEN
        assertThat(dirObtainedFromPath).isEqualTo(dirPath);
    }

    @Test
    public void shouldAllowAccessingFileNameOfThePath() {
        //GIVEN
        val dirPath = Any.instanceOf(AbsoluteDirectoryPath.class);
        val fileName = Any.anonymous(fileName());
        AbsoluteFilePath absoluteFilePath = dirPath.with(fileName);

        //WHEN
        val fileNameFromPath = absoluteFilePath.fileName();

        //THEN
        assertThat(fileNameFromPath).isEqualTo(fileName);
    }

    @Test
    public void ShouldAllowGettingPathRoot()
    {
        //GIVEN
        val pathString = "C:\\lolek\\lol.txt";
        val pathWithFilename = AbsoluteFilePath.from(pathString);

        //WHEN
        val root = pathWithFilename.root();

        //THEN
        assertThat(root).isEqualTo(
            AbsoluteDirectoryPath.from(
                Paths.get(pathString).getRoot().toString()));
    }


    /*

    @Test
    public void ShouldBeConvertibleToAnyPathWithFileName()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<AbsoluteFilePath>();

      //WHEN
      AnyFilePath anyFilePath = pathWithFileName.AsAnyFilePath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyFilePath.ToString());
    }

    @Test
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<AbsoluteFilePath>();

      //WHEN
      AnyPath anyPathWithFileName = pathWithFileName.AsAnyPath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyPathWithFileName.ToString());
    }


    [Theory,
     InlineData(@"C:\Dir\Subdir\fileName.txt", ".txt", true),
     InlineData(@"C:\Dir\Subdir\fileName.tx", ".txt", false),
     InlineData(@"C:\Dir\Subdir\fileName", ".txt", false),
    ]
    public void ShouldBeAbleToRecognizeWhetherItHasCertainExtension(string path, string extension, bool expectedResult)
    {
      //GIVEN
      var pathWithFileName = AbsoluteFilePath.Value(path);
      var extensionValue = FileExtension.Value(extension);

      //WHEN
      var hasExtension = pathWithFileName.Has(extensionValue);

      //THEN
      Assert.Equal(expectedResult, hasExtension);
    }

    @Test
    public void ShouldAllowChangingExtension()
    {
      //GIVEN
      var filePath = AbsoluteFilePath.Value(@"C:\Dir\subdir\file.txt");

      //WHEN
      AbsoluteFilePath pathWithNewExtension = filePath.ChangeExtensionTo(FileExtension.Value(".doc"));

      //THEN
      Assert.Equal(@"C:\Dir\subdir\file.doc", pathWithNewExtension.ToString());

    }

    @Test
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<AbsoluteFilePath>();
      var path2 = Any.Instance<AbsoluteFilePath>();
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