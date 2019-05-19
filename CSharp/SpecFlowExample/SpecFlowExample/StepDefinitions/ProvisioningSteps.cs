using SpecFlowExample.AutomationLayer.Actors;
using SpecFlowExample.AutomationLayer.Infrastructure;
using TechTalk.SpecFlow;

namespace SpecFlowExample.StepDefinitions
{
  [Binding]
  public class ProvisioningSteps
  {
    private readonly ChatScenarioContext _context;

    public ProvisioningSteps(ChatScenarioContext context)
    {
      _context = context;
    }

    [Given(@"the following users:")]
    public void GivenTheFollowingUsers(Table table)
    {
      foreach (var row in table.Rows)
      {
        var user = new User(row[0]);
        user.RegisterInSystem();
        _context.Register(user);
      }
    }

  }
}