using System;
using System.Threading;
using System.Threading.Tasks;
using GameBot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Schema;
using NSubstitute;
using Xunit;

namespace IntegrationSpecification
{
  /*
  public class UnitTest1
  {
    [Fact]
    public async Task Test1()
    {
      var luisUserPhrase = 
        new LuisUserPhrase(TurnContextWith("AAA"), Startup.CreateLuisRecognizer());

      var recognizeIntent = await luisUserPhrase.RecognizeIntentAsync(CancellationToken.None);
    }

    private static ITurnContext TurnContextWith(string text)
    {
      var turnContext = Substitute.For<ITurnContext>();
      turnContext.Activity.Returns(new Activity(ActivityTypes.Message, text: text));
      return turnContext;
    }
  }*/
}
