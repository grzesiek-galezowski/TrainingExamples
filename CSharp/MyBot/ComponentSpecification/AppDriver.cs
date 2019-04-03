using System.Threading;
using System.Threading.Tasks;
using BotLogic;
using BotLogic.Composition;
using BotLogic.States;
using FluentAssertions;
using NSubstitute;

namespace ComponentSpecification
{
  public class AppDriver
  {
    private readonly IActivityFactory _activityFactory;
    private FakeConversationPartner? _conversationPartner;
    private readonly IBotPersistentState _botPersistentState;
    private readonly IUserPhrase _userPhrase;

    public AppDriver()
    {
      _activityFactory = new ActivityFactory();
      _botPersistentState = new FakeBotPersistentState();
      _userPhrase = Substitute.For<IUserPhrase>();
      
    }

    public async Task Receives(RecognitionResultDto recognitionResultDto)
    {
      _conversationPartner = new FakeConversationPartner();
      _userPhrase.RecognizeIntentAsync(Arg.Any<CancellationToken>()).Returns(recognitionResultDto);

      var messageActivity = await _activityFactory.CreateMessageActivityAsync(
        _botPersistentState, _userPhrase, _conversationPartner, CancellationToken.None);
      await messageActivity.HandleAsync(CancellationToken.None);
    }

    public void Answers(string expectedAnswer)
    {
      _conversationPartner.Content.Should().Be(expectedAnswer);
    }
  }
}