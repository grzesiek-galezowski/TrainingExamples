using System.Threading.Tasks;

namespace BotBuilderEchoBotV4
{
    public class GameCatalog
    {
        public Task<Games> GetGamesAsync()
        {
            return Task.FromResult(new Games("FF6","FF7", "FF8"));
        }
    }
}