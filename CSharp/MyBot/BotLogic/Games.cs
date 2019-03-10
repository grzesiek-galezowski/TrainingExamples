using BotLogic;

namespace BotBuilderEchoBotV4.Logic
{
    public class Games
    {
        private readonly string[] _games;

        public Games(params string[] games)
        {
            _games = games;
        }

        public void DisplayFor(BotLogic.IUser user)
        {
            foreach (var game in _games)
            {
                user.AppendToResponseAsync(game);
            }
        }
    }

}