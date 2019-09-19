using System;
using System.Linq;
using FluentAssertions;
using TddXt.SimpleNlp;
using Xunit;

namespace SimpleNlpSpecification
{
  public class IntentRecognitionSpecification
  {
    [Fact]
    public void ShouldRecognizeNoIntentWhenNothingHasBeenConfigured()
    {
      //GIVEN
      var model = new RecognitionModel();
      
      //WHEN
      var recognitionResult = model.Recognize("Trolololo");

      //THEN
      recognitionResult.TopIntent.Should().Be("None");
    }

    [Fact]
    public void ShouldRecognizeNoIntentWhenTextDoesNotContainAnyOfIntentEntities()
    {
      //GIVEN
      var model = new RecognitionModel();
      
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
      var model = new RecognitionModel();
      
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
      var model = new RecognitionModel();
      
      model.AddEntity(EntityName.Value("YES"), "yes");
      model.AddEntity(EntityName.Value("PLEASE"), "please");
      model.AddIntent("INTENT_YES", new[] { EntityName.Value("YES"), EntityName.Value("PLEASE")});

      //WHEN
      var recognitionResult = model.Recognize("yes, please");

      //THEN
      recognitionResult.TopIntent.Should().Be("INTENT_YES");
    }

    [Fact]
    public void ShouldReturnFirstExactlyMatchedIntent()
    {
      //GIVEN
      var model = new RecognitionModel();
      
      model.AddEntity(EntityName.Value("YES"), "yes");
      model.AddEntity(EntityName.Value("PLEASE"), "please");
      model.AddIntent("INTENT_YES1", new[] { EntityName.Value("YES"), EntityName.Value("PLEASE")});
      model.AddIntent("INTENT_YES2", new[] { EntityName.Value("YES"), EntityName.Value("PLEASE")});

      //WHEN
      var recognitionResult = model.Recognize("yes, please");

      //THEN
      recognitionResult.TopIntent.Should().Be("INTENT_YES1");
    }

    [Fact]
    public void ShouldBeAbleToMatchMultipleIntents()
    {
      //GIVEN
      var model = new RecognitionModel();
      
      model.AddEntity(EntityName.Value("YES"), "yes");
      model.AddEntity(EntityName.Value("NO"), "no");
      model.AddEntity(EntityName.Value("START_OVER"), "start over");
      model.AddEntity(EntityName.Value("GAME_OVER"), "game over");
      model.AddIntent("INTENT_YES", new[] { EntityName.Value("YES") });
      model.AddIntent("INTENT_NO", new[] { EntityName.Value("NO")});
      model.AddIntent("INTENT_START_OVER", new[] { EntityName.Value("START_OVER")});
      model.AddIntent("INTENT_GAME_OVER", new[] { EntityName.Value("GAME_OVER")});

      //WHEN
      var recognitionResult1 = model.Recognize("yes, please");
      var recognitionResult2 = model.Recognize("no, thank you");
      var recognitionResult3 = model.Recognize("game over, amigo");
      var recognitionResult4 = model.Recognize("shall we start over?");

      //THEN
      recognitionResult1.TopIntent.Should().Be("INTENT_YES");
      recognitionResult2.TopIntent.Should().Be("INTENT_NO");
      recognitionResult3.TopIntent.Should().Be("INTENT_GAME_OVER");
      recognitionResult4.TopIntent.Should().Be("INTENT_START_OVER");
    }
    
    [Fact]
    public void ShouldBeAbleToMatchMultipleIntentPatterns()
    {
      //GIVEN
      var model = new RecognitionModel();
      
      model.AddEntity(EntityName.Value("YES"), "yes");
      model.AddEntity(EntityName.Value("NO"), "no");
      model.AddEntity(EntityName.Value("START_OVER"), "start over");
      model.AddEntity(EntityName.Value("GAME_OVER"), "game over");
      model.AddIntent("INTENT", new[] { EntityName.Value("YES") });
      model.AddIntent("INTENT", new[] { EntityName.Value("NO")});
      model.AddIntent("INTENT", new[] { EntityName.Value("START_OVER")});
      model.AddIntent("INTENT_GAME_OVER", new[] { EntityName.Value("GAME_OVER")});

      //WHEN
      var recognitionResult1 = model.Recognize("yes, please");
      var recognitionResult2 = model.Recognize("no, thank you");
      var recognitionResult3 = model.Recognize("game over, amigo");
      var recognitionResult4 = model.Recognize("shall we start over?");

      //THEN
      recognitionResult1.TopIntent.Should().Be("INTENT");
      recognitionResult2.TopIntent.Should().Be("INTENT");
      recognitionResult3.TopIntent.Should().Be("INTENT_GAME_OVER");
      recognitionResult4.TopIntent.Should().Be("INTENT");
    }

    //TODO approximate matching
    //TODO intent not matched
    //TODO conflicts
    //TODO what if no intent matches all entities?
    //TODO Intents with counts on entites (e.g. ENTITY_X, AtLeast(1.Times()))
  }
}
