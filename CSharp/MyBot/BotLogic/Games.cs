using System.Threading.Tasks;

namespace BotLogic
{
    public class Games
    {
        private readonly string[] _games;

        public Games(params string[] games)
        {
            _games = games;
        }

        public void DisplayFor(IConversationPartner conversationPartner)
        {
            foreach (var game in _games)
            {
                conversationPartner.AppendToResponse(game);
            }
        }
    }

}