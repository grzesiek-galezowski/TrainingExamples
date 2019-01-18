namespace BotBuilderEchoBotV4
{
    public class Games
    {
        private readonly string[] _games;

        public Games(params string[] games)
        {
            _games = games;
        }

        public void DisplayFor(User user)
        {
            foreach (var game in _games)
            {
                user.SayAsync(game);
            }
        }
    }
}