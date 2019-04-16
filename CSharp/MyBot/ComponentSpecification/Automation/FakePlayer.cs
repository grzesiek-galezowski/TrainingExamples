using System.Threading;
using System.Threading.Tasks;
using BotLogic;

namespace ComponentSpecification.Automation
{
  public class FakePlayer : IPlayer
  {
    public string Content { get; private set; } = string.Empty;

    public void AppendToResponse(string role, string text)
    {
      Content += text;
    }

    public Task RespondAsync(CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }
  }
}