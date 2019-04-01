using System.Threading;
using System.Threading.Tasks;
using BotBuilderEchoBotV4;
using BotLogic;
using BotLogic.States;
using NSubstitute;
using Xunit;

namespace ComponentSpecification
{
  public class AppDriver
  {
    private readonly IActivityFactory _activityFactory;
    private readonly IConversationPartner _conversationPartner;
    private readonly IBotPersistentState _botPersistentState;
    private readonly IUserPhrase _userPhrase;

    public AppDriver()
    {
      _activityFactory = new ActivityFactory();
      _conversationPartner = Substitute.For<IConversationPartner>();
      _botPersistentState = Substitute.For<IBotPersistentState>();
      _userPhrase = Substitute.For<IUserPhrase>();
      
      //dummy line:
      _userPhrase.RecognizeIntentAsync(Arg.Any<CancellationToken>()).Returns(new RecognitionResultDto()
      {
        Intent = "catalog"
      });
    }

    public async Task SendMessageAsync()
    {
      var messageActivity = await _activityFactory.CreateMessageActivityAsync(
        _botPersistentState, _userPhrase, _conversationPartner, CancellationToken.None);
      await messageActivity.HandleAsync(CancellationToken.None);
    }
  }

  public class UnitTest1
  {
    [Fact]
    public async Task Test1()
    {
      await new AppDriver().SendMessageAsync();
    }

  }

}
