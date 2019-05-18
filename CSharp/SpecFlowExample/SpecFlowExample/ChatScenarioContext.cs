using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace SpecFlowExample
{
  /// <summary>
  /// Specflow has its own "locator" in ScenarioContext.Current.ScenarioContainer
  /// But honestly I hate it ;-) so I always come up with my own
  /// </summary>
  public class ChatScenarioContext
  {
    private readonly List<User> _users = new List<User>();

    public void Register(User user)
    {
      _users.Should().NotContain(user);
      _users.Add(user);
    }

    public User LocateUser(string userName)
    {
      return _users.Single(u => u.HasName(userName));
    }

    public User UserInTheSpotlight()
    {
      return _users.Last();
    }
  }
}