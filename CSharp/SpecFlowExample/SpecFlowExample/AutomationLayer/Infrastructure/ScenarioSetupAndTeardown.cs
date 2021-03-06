﻿using TechTalk.SpecFlow;

namespace SpecFlowExample.AutomationLayer.Infrastructure
{
  [Binding]
  public class ScenarioSetupAndTeardown
  {
    private readonly ScenarioContext _context;
    private ChatScenarioContext _chatScenarioContext;

    public ScenarioSetupAndTeardown(ScenarioContext context)
    {
      _context = context;
    }

    [BeforeScenario]
    public void Setup()
    {
      _chatScenarioContext = new ChatScenarioContext();
      _context.ScenarioContainer.RegisterInstanceAs(_chatScenarioContext);
    }

    [AfterScenario]
    public void Teardown()
    {
      _chatScenarioContext.RemoveChatRooms();
      _chatScenarioContext.RemoveUsers();
    }
  }
}