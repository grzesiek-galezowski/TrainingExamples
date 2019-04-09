using System.Threading;
using System.Threading.Tasks;
using BotLogic;
using BotLogic.Composition;
using BotLogic.States;
using FluentAssertions;
using NSubstitute;

namespace ComponentSpecification.Automation
{
  public class AppDriver
  {
    private readonly IActivityFactory _activityFactory;
    private FakePlayer? _player;
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
      _player = new FakePlayer();
      _userPhrase.RecognizeIntentAsync(Arg.Any<CancellationToken>()).Returns(recognitionResultDto);

      var messageActivity = await _activityFactory.CreateMessageActivityAsync(
        _botPersistentState, _userPhrase, _player, CancellationToken.None);
      await messageActivity.HandleAsync(CancellationToken.None);
    }

    public void AnswersWith(params string[] expectedAnswer)
    {
      _player!.Content.Should().Be(string.Join("", expectedAnswer));
    }
  }
}