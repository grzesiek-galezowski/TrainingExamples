using System.Threading.Tasks;
using BotLogic;
using Xunit;

namespace ComponentSpecification
{
  public class UnitTest1
  {
    [Fact]
    public async Task Test1()
    {
      var bot = new AppDriver();
      await bot.Receives(Intents.StartGame());
      bot.Answers(BrightRoomConversations.EntryDescription());
      await bot.Receives(Intents.Kill("Gandalf"));
      bot.Answers(BrightRoomConversations.AttemptingToKillGandalfAnswer());
    }
  }

}
