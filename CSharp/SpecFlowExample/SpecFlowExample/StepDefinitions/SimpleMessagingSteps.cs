using System.Linq;
using SpecFlowExample.AutomationLayer.Data;
using SpecFlowExample.AutomationLayer.Infrastructure;
using TechTalk.SpecFlow;

namespace SpecFlowExample.StepDefinitions
{
  [Binding]
  public class SimpleMessagingSteps
  {
    private readonly ChatScenarioContext _context;

    public SimpleMessagingSteps(ChatScenarioContext context)
    {
      _context = context;
    }

    [When(@"they send the following messages in order:")]
    public void WhenTheySendTheFollowingMessagesInOrder(Table table)
    {
      foreach (var tableRow in table.Rows)
      {
        var from = _context.LocateUser(tableRow[0]);
        var to = _context.LocateUser(tableRow[1]);
        var messageText = tableRow[2];

        from.SendDirectMessage(to, messageText);
      }
    }

    [Then(@"(.*) should see:")]
    public void ThenUserShouldSee(string userName, Table table)
    {
      var expectedMessages = table.Rows.Select(r => new DisplayedMessage(r[0], r[1], r[2]));
      
      _context.LocateUser(userName).ShouldSee(expectedMessages);
    }

  }
}