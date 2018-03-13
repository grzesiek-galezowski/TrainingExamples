package logic;

import autofixture.publicinterface.Any;
import lombok.val;
import org.testng.annotations.Test;

import static com.github.grzesiek_galezowski.test_environment.XAssertJConditions.corectlyImplementedEquality;
import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

public class FileNameSpecification {

    @Test
    public void shouldNotAllowToBeCreatedWithNullValue() {
        assertThatThrownBy(() -> FileName.from(null))
            .isInstanceOf(NullPointerException.class);
    }

    @Test
    public void shouldThrowExceptionWhenCreatedWithStringContainingMoreThanJustAFileName() {
        assertThatThrownBy(() ->
            FileName.from("c:\\lolek\\lolki2.txt"))
            .isInstanceOf(IllegalArgumentException.class);
    }

    @Test
    public void shouldHaveProperEquality() {
        assertThat(FileName.class).has(corectlyImplementedEquality());
    }

    @Test
    public void shouldReturnTheStringItWasCreatedWithWhenConvertedToString() {
        //GIVEN
        val initialValue = Any.alphaString();
        val path = FileName.from(initialValue);

        //WHEN
        val convertedToString = path.toString();

        //THEN
        assertThat(convertedToString).isEqualTo(initialValue);
    }

    /*



    @Test
    public void ShouldAllowGettingExtensionWhenItExists()
    {
      //GIVEN
      var fileNameWithoutExtensionString = Any.String();
      var extensionString = "." + Any.String();
      var fileNameWithExtensionString = fileNameWithoutExtensionString + extensionString;

      var fileNameWithExtension = FileName.Value(fileNameWithExtensionString);

      //WHEN
      var maybeExtension = fileNameWithExtension.Extension();

      //THEN
      Assert.True(maybeExtension.Found);
      Assert.Equal(FileExtension.Value(extensionString), maybeExtension.Value());
    }

    @Test
    public void ShouldYieldNoExtensionWhenThePathHasNoExtension()
    {
      //GIVEN
      var fileNameWithoutExtensionString = Any.String();

      var fileNameWithoutExtension = FileName.Value(fileNameWithoutExtensionString);

      //WHEN
      var maybeExtension = fileNameWithoutExtension.Extension();

      //THEN
      Assert.False(maybeExtension.Found);
      Assert.Throws<InvalidOperationException>(() => maybeExtension.Value());
    }

    @Test
    public void ShouldAllowAccessingFileNameWithoutExtension()
    {
      //GIVEN
      var fileNameWithoutExtensionString = Any.String();
      var extensionString = "." + Any.String();
      var fileNameWithExtensionString = fileNameWithoutExtensionString + extensionString;

      var fileNameWithExtension = FileName.Value(fileNameWithExtensionString);

      //WHEN
      var fileNameWithoutExtension = fileNameWithExtension.WithoutExtension();

      //THEN
      Assert.Equal(FileNameWithoutExtension.Value(fileNameWithoutExtensionString), fileNameWithoutExtension);

    }

    @Test
    public void ShouldAllowChangingExtension()
    {
      //GIVEN
      var fileName = FileName.Value(@"file.txt");

      //WHEN
      FileName nameWithNewExtension = fileName.ChangeExtensionTo(FileExtension.Value(".doc"));

      //THEN
      Assert.Equal(@"file.doc", nameWithNewExtension.ToString());

    }

    @Test
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<FileName>();
      var path2 = Any.Instance<FileName>();
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