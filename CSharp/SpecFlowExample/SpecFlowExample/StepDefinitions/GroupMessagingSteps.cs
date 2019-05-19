using SpecFlowExample.AutomationLayer.Actors;
using SpecFlowExample.AutomationLayer.Infrastructure;
using TechTalk.SpecFlow;

namespace SpecFlowExample.StepDefinitions
{
  [Binding]
  public class GroupMessagingSteps
  {
    private readonly ChatScenarioContext _context;

    public GroupMessagingSteps(ChatScenarioContext context)
    {
      _context = context;
    }

    [Given(@"a chat room ""(.*)"" that contains:")]
    public void GivenAChatRoomThatContains(string chatRoomName, Table table)
    {
      var chatRoom = new ChatRoom(chatRoomName);
      _context.Register(chatRoom);
      foreach (var row in table.Rows)
      {
        var user = _context.LocateUser(row[0]);
        chatRoom.Add(user);
      }
    }

    [Then(@"(.*) should see nothing")]
    public void ThenSomeOtherUserShouldSeeNothing(string userName)
    {
      var chatRoom = _context.ChatRoomInTheSpotlight();
      chatRoom.ShouldNotContainAnyMessagesFor(userName);
    }

    [When(@"(.*) sends a message ""(.*)"" to chat room ""(.*)""")]
    public void WhenUserSendsAMessageToChatRoom(
      string senderName, string messageText, string chatRoomName)
    {
      var sender = _context.LocateUser(senderName);
      var recipient = _context.LocateChatRoom(chatRoomName);

      sender.SendDirectMessage(recipient, messageText);
    }

  }
}
