using System.Configuration;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowExample
{
  [Binding]
  public class SimpleMessagingSteps
  {
    private ChatScenarioContext _context;

    [Given(@"a user (.*)")]
    public void GivenAUser(string name)
    {
      var user = new User(name);
      _context.Register(user);
    }

    [Given(@"that he is a friend of (.*)")]
    public void GivenThatHeIsAFriendOf(string userName)
    {
      _context.UserInTheSpotlight().AddFriend(_context.LocateUser(userName));
    }

    [Given(@"that he is not a friend of Johnny")]
    public void GivenThatHeIsNotAFriendOfJohnny()
    {
      ScenarioContext.Current.Pending();
    }

    [When(@"They send the following messages in order:")]
    public void WhenTheySendTheFollowingMessagesInOrder(Table table)
    {
      ScenarioContext.Current.Pending();
    }

    [When(@"Johnny sends a message ""(.*)"" to Benjamin")]
    public void WhenJohnnySendsAMessageToBenjamin(string p0)
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"Johnny should see:")]
    public void ThenJohnnyShouldSee(Table table)
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"Benjamin should see:")]
    public void ThenBenjaminShouldSee(Table table)
    {
      ScenarioContext.Current.Pending();
    }
  }
}