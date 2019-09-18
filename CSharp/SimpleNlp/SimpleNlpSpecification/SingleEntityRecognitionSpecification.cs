using FluentAssertions;
using SimpleNlp;
using Xunit;

namespace SimpleNlpSpecification
{
  public class SingleEntityRecognitionSpecification
  {
    //TODO conflicts?
    
    [Theory]
    [InlineData("DRIVER_LICENSE", "driver license", "driver license")]
    [InlineData("DRIVER_LICENSE", "driver license", "Driver License")] //does it ignore letter casing?
    [InlineData("DRIVER_LICENSE", "driver license", "driver  license")] // does it normalize spaces?
    [InlineData("LICENSE_PLATE", "license plate", "license plate")] //works for other values?
    [InlineData("LICENSE_PLATE", "license plate", "give me a license plate")] //works for prefixed values?
    [InlineData("LICENSE_PLATE", "license plate", "license plate, will ya?")] //works for suffixed values?
    public void ShouldBeAbleToRecognizeSingleEntity(string entityName, string entityValue, string text)
    {
      var model = new Model();
      model.AddEntity(EntityName.Value(entityName), entityValue);

      var result = model.Recognize(text);

      result.ShouldContainOnly(entityName, entityValue);
    }

    [Fact]
    public void ShouldBeAbleToRecognizeSingleEntityWithSynonyms()
    {
      var model = new Model();
      model.AddEntity(EntityName.Value("DRIVER_LICENSE"), "driver license");
      model.AddEntity(EntityName.Value("DRIVER_LICENSE"), "driver licence");
      model.AddEntity(EntityName.Value("DRIVER_LICENSE"), "driving license");

      var result1 = model.Recognize("driver license");
      var result2 = model.Recognize("driver licence");
      var result3 = model.Recognize("driving license");

      result1.ShouldContainOnly("DRIVER_LICENSE", "driver license");
      result2.ShouldContainOnly("DRIVER_LICENSE", "driver licence");
      result3.ShouldContainOnly("DRIVER_LICENSE", "driving license");
    }

    [Fact]
    public void ShouldReturnEmptyListWhenNoEntitiesWereDefined()
    {
      var model = new Model();

      var result1 = model.Recognize("driver license");

      result1.Entities.Should().BeEmpty();
    }

    [Fact]
    public void ShouldReturnEmptyListWhenNoTextDoesNotMatch()
    {
      var model = new Model();
      model.AddEntity(EntityName.Value("DRIVER_LICENSE"), "driver license");

      var result = model.Recognize("Trolololo");

      result.Entities.Should().BeEmpty();
    }

  }
}
