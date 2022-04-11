namespace MockNoMock.Twitter;

public class NewTweetCommand : ITweetCommand
{
  private readonly INewTweetRequest _newTweet;
  private readonly IPostedTweets _postedTweets;
  private readonly IAddingOperationStatus _operationStatus;
  private readonly IFollowers _followers;

  public NewTweetCommand(
    INewTweetRequest newTweet,
    IPostedTweets postedTweets,
    IFollowers followers,
    IAddingOperationStatus operationStatus)
  {
    _newTweet = newTweet;
    _postedTweets = postedTweets;
    _operationStatus = operationStatus;
    _followers = followers;
  }

  public async Task Execute()
  {
    _newTweet.AssertContentIsOfRequiredLength();
    _newTweet.AssertContentContainsNoInappropriateWords();
    await _newTweet.AddTo(_postedTweets, _operationStatus);
    await _newTweet.Notify(_followers, _operationStatus);
  }
}