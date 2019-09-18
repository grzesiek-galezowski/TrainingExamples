using System;
using System.Linq;
using FluentAssertions;
using SimpleNlp;
using Xunit;

namespace SimpleNlpSpecification
{
  public class IntentRecognitionSpecification
  {
    [Fact]
    public void ShouldRecognizeNoIntentWhenNothingHasBeenConfigured()
    {
      //GIVEN
      var model = new Model();
      
      //WHEN
      var recognitionResult = model.Recognize("Trolololo");

      //THEN
      recognitionResult.TopIntent.Should().Be("None");
    }

    [Fact]
    public void ShouldRecognizeNoIntentWhenTextDoesNotContainAnyOfIntentEntities()
    {
      //GIVEN
      var model = new Model();
      
      model.AddEntity(EntityName.Value("NO"), "no");
      model.AddIntent("INTENT_REFUSE", new [] { EntityName.Value("NO") });

      //WHEN
      var recognitionResult = model.Recognize("Trolololo");

      //THEN
      recognitionResult.TopIntent.Should().Be("None");
    }

    [Theory]
    [InlineData("yes")]
    [InlineData("yes no")]
    [InlineData("no yes")] //does it work when the defining entity is not the first one?
    public void ShouldRecognizeIntentWithSingleEntity(string text)
    {
      //GIVEN
      var model = new Model();
      
      model.AddEntity(EntityName.Value("YES"), "yes");
      model.AddEntity(EntityName.Value("NO"), "no");
      var entityNames = new [] { EntityName.Value("YES")};
      model.AddIntent("INTENT_YES", entityNames);

      //WHEN
      var recognitionResult = model.Recognize(text);

      //THEN
      recognitionResult.TopIntent.Should().Be("INTENT_YES");
    }

    [Fact]
    public void ShouldRecognizeIntentWithMultipleEntitiesMatchingExactlyTheOnesRecognized()
    {
      //GIVEN
      var model = new Model();
      
      model.AddEntity(EntityName.Value("YES"), "yes");
      model.AddEntity(EntityName.Value("PLEASE"), "please");
      model.AddIntent("INTENT_YES", new[] { EntityName.Value("YES"), EntityName.Value("PLEASE")});

      //WHEN
      var recognitionResult = model.Recognize("yes, please");

      //THEN
      recognitionResult.TopIntent.Should().Be("INTENT_YES");
    }

    //TODO no matching intent
    //TODO matching single on any position
    //TODO matching multiple
    //TODO matching multiple on any position
    //bug intent not matched
    //bug more than one entity-based intent 
    //bug several alternatives for an intent
    //bug what if no intent matches all entities?
  }
}
