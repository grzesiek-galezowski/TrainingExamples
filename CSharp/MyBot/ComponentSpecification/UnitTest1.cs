using System;
using System.Threading.Tasks;
using BotBuilderEchoBotV4;
using BotLogic;
using BotLogic.States;
using NSubstitute;
using TddXt.AnyRoot.Strings;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace ComponentSpecification
{
  public class AppDriver
  {
    private readonly ActivityFactory _activityFactory;
    private readonly IConversationPartner _conversationPartner;
    private readonly IBotPersistentState _botPersistentState;

    public AppDriver()
    {
      _activityFactory = new ActivityFactory();
      _conversationPartner = Substitute.For<IConversationPartner>();
      _botPersistentState = Substitute.For<IBotPersistentState>();
    }

    public async Task SendMessage(string activityText)
    {
      var messageActivity = await _activityFactory.CreateMessageActivity(
        _botPersistentState, _conversationPartner, activityText);
      await messageActivity.HandleAsync();
    }
  }

  public class UnitTest1
  {
    [Fact]
    public async Task Test1()
    {
      await new AppDriver().SendMessage(Any.String());
    }
  }
}
