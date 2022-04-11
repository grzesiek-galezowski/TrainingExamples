namespace MockNoMock.Twitter;

public interface INewTweetRequest
{
  void AssertContentIsOfRequiredLength();
  void AssertContentContainsNoInappropriateWords();
  Task AddTo(IPostedTweets usersTweets, IAddingToExistingTweetsStatus operationStatus);
  Task Notify(IFollowers followers, INotifyingFollowersStatus operationStatus);
}