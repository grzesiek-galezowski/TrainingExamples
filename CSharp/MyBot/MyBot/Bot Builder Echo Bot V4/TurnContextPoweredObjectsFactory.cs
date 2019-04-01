using BotBuilderEchoBotV4.Adapters;
using BotLogic;
using BotLogic.States;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;

namespace BotBuilderEchoBotV4
{
  public interface ITurnContextPoweredObjectsFactory
  {
    IConversationPartner CreateConversationPartner(ITurnContext turnContext);
    IBotPersistentState CreateBotPersistentState(ITurnContext turnContext);
    IUserPhrase ExtractUserPhraseFrom(ITurnContext turnContext);
  }

  public class TurnContextPoweredObjectsFactory : ITurnContextPoweredObjectsFactory
  {
    private readonly BotAccessors _accessors;
    private readonly LuisRecognizer _luisRecognizer;

    public TurnContextPoweredObjectsFactory(BotAccessors accessors, LuisRecognizer luisRecognizer)
    {
      _accessors = accessors;
      _luisRecognizer = luisRecognizer;
    }

    public IConversationPartner CreateConversationPartner(ITurnContext turnContext)
    {
      return new BotBuilderConversationPartner(turnContext);
    }

    public IBotPersistentState CreateBotPersistentState(ITurnContext turnContext)
    {
      return new BotPersistentState(turnContext, _accessors);
    }

    public IUserPhrase ExtractUserPhraseFrom(ITurnContext turnContext)
    {
      return new LuisUserPhrase(turnContext, _luisRecognizer);
    }
  }
}