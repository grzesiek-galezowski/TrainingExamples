using System.Threading;
using System.Threading.Tasks;
using BotLogic;

namespace ComponentSpecification
{
  public class FakeConversationPartner : IConversationPartner
  {
    public string Content { get; private set; } = string.Empty;

    public void AppendToResponse(string text)
    {
      Content += text;
    }

    public Task RespondAsync(CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }
  }
}