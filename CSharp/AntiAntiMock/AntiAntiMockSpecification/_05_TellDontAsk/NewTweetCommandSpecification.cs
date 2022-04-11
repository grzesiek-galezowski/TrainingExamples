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
    var operationStatus = Any.Instance<IAddingOperationStatus>();
    var newTweetCommand = new NewTweetCommand(
      newTweet,
      postedTweets,
      followers,
      operationStatus);

    //WHEN
    await newTweetCommand.Execute();

    //THEN
    Received.InOrder(() =>
    {
      newTweet.AssertContentIsOfRequiredLength();
      newTweet.AssertContentContainsNoInappropriateWords();
      newTweet.AddTo(postedTweets, operationStatus);
      newTweet.Notify(followers, operationStatus);
    });
  }
}