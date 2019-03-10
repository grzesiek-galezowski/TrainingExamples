using System.Threading.Tasks;

namespace BotLogic
{
    public class GameCatalog
    {
        public Task<Games> GetGamesAsync()
        {
            return Task.FromResult(new Games("FF6","FF7", "FF8"));
        }
    }
}