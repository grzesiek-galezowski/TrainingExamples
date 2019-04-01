using System.Threading;
using System.Threading.Tasks;

namespace BotLogic
{
  public interface IUserPhrase
  {
    Task<RecognitionResultDto> RecognizeIntentAsync(CancellationToken cancellationToken);
  }
}