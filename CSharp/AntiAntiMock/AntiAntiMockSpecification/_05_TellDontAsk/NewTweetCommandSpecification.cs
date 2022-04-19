using System.Threading.Tasks;
using MockNoMock.Twitter;

namespace MockNoMockSpecification._05_TellDontAsk;

internal class NewTweetCommandSpecification
{
  [Test]
  public async Task ShouldValidateAndPostTweetThenNotifyAllFollowers()
  {
    //GIVEN
    var newTweet = Substitute.For<INewTweetRequest>();
    var postedTweets = Any.Instance<IPostedTweets>();
    var followers = Any.Instance<IFollowers>();
    var runningOperationStatus = Any.Instance<IAddingOperationStatus>();
    var newTweetCommand = new NewTweetCommand(
      newTweet,
      postedTweets,
      followers,
      runningOperationStatus);

    //WHEN
    await newTweetCommand.Execute();

    //THEN
    Received.InOrder(() =>
    {
      newTweet.AssertContentIsOfRequiredLength();
      newTweet.AssertContentContainsNoInappropriateWords();
      newTweet.AddTo(postedTweets, runningOperationStatus);
      newTweet.Notify(followers, runningOperationStatus);
    });
  }
}