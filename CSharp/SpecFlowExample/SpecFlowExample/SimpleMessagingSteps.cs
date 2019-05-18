using System.Configuration;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowExample
{
  [Binding]
  public class SimpleMessagingSteps
  {
    private readonly ChatScenarioContext _context;

    public SimpleMessagingSteps(ChatScenarioContext context)
    {
      _context = context;
    }

    [Given(@"a user (.*)")]
    public void GivenAUser(string name)
    {
      var user = new User(name);
      user.RegisterInSystem();
      _context.Register(user);
    }

    [Given(@"that he is a friend of (.*)")]
    public void GivenThatHeIsAFriendOf(string userName)
    {
      _context.UserInTheSpotlight().AddFriend(_context.LocateUser(userName));
    }

    [When(@"They send the following messages in order:")]
    public void WhenTheySendTheFollowingMessagesInOrder(Table table)
    {
      foreach (var tableRow in table.Rows)
      {
        var from = _context.LocateUser(tableRow[0]);
        var to = _context.LocateUser(tableRow[1]);
        var messageText = tableRow[3];

        from.SendDirectMessage(to, messageText);
      }
    }

    [When(@"(.*) sends a message ""(.*)"" to (.*)")]
    public void WhenJohnnySendsAMessageToBenjamin(
      string senderName, string messageText, string recipientName)
    {
      var sender = _context.LocateUser(senderName);
      var recipient = _context.LocateUser(recipientName);

      sender.SendDirectMessage(recipient, messageText);
    }

    [Then(@"(.*) should see:")]
    public void ThenJohnnyShouldSee(string userName, Table table)
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