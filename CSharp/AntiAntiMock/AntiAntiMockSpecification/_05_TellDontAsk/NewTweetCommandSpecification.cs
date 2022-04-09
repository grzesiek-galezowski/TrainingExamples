using System.Threading.Tasks;

namespace MockNoMockSpecification._05_TellDontAsk;

internal class NewTweetCommandSpecification
{
  [Test]
  public async Task ShouldValidateAndPostTweetThenNotifyAllFollowers()
  {
    //GIVEN
    var newTweet = Substitute.For<INewTweetRequest>();
    var existingTweets = Any.Instance<IExistingTweets>();
    var followers = Any.Instance<IFollowers>();
    var operationStatus = Any.Instance<IAddingOperationStatus>();
    var newTweetCommand = new NewTweetCommand(
      newTweet,
      existingTweets,
      followers,
      operationStatus);

    //WHEN
    await newTweetCommand.ExecuteAsync();

    //THEN
    Received.InOrder(() =>
    {
      newTweet.AssertContentIsOfRequiredLength();
      newTweet.AssertContentContainsNoInappropriateWords();
      newTweet.AddTo(existingTweets, operationStatus);
      newTweet.Notify(followers, operationStatus);
    });
  }
}

internal class NewTweetCommand : ITweetCommand
{
  private readonly INewTweetRequest _newTweet;
  private readonly IExistingTweets _existingTweets;
  private readonly IAddingOperationStatus _operationStatus;
  private readonly IFollowers _followers;

  public NewTweetCommand(
    INewTweetRequest newTweet,
    IExistingTweets existingTweets,
    IFollowers followers,
    IAddingOperationStatus operationStatus)
  {
    _newTweet = newTweet;
    _existingTweets = existingTweets;
    _operationStatus = operationStatus;
    _followers = followers;
  }

  public async Task ExecuteAsync()
  {
    _newTweet.AssertContentIsOfRequiredLength();
    _newTweet.AssertContentContainsNoInappropriateWords();
    await _newTweet.AddTo(_existingTweets, _operationStatus);
    await _newTweet.Notify(_followers, _operationStatus);
  }
}

public interface IFollowers
{
}

public interface IAddingOperationStatus : INotifyingFollowersStatus, IAddingToExistingTweetsStatus
{
}

public interface IExistingTweets
{
}

public interface INewTweetRequest
{
  void AssertContentIsOfRequiredLength();
  void AssertContentContainsNoInappropriateWords();
  Task AddTo(IExistingTweets usersTweets, IAddingToExistingTweetsStatus operationStatus);
  Task Notify(IFollowers followers, INotifyingFollowersStatus operationStatus);
}

public interface INotifyingFollowersStatus
{
}

public interface IAddingToExistingTweetsStatus
{
}

public interface ITweetCommand
{
}