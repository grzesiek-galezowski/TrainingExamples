using System.Threading.Tasks;
using BotLogic;
using Xunit;

namespace ComponentSpecification
{
  public class UnitTest1
  {
    [Fact]
    public async Task ShouldAllowTryingToKillGandalfAndEndGame()
    {
      var bot = new AppDriver();
      await bot.Receives(Intents.StartGame());
      bot.Answers(BrightRoomConversations.EntryDescription());
      await bot.Receives(Intents.Kill("Gandalf"));
      bot.Answers(BrightRoomConversations.AttemptingToKillGandalfAnswer());
      await bot.Receives(Intents.StartGame());
      bot.Answers(BrightRoomConversations.EntryDescription());
    }

    [Fact]
    public async Task ShouldAllowTryingToKillAragornAndEndGame()
    {
      var bot = new AppDriver();
      await bot.Receives(Intents.StartGame());
      bot.Answers(BrightRoomConversations.EntryDescription());
      await bot.Receives(Intents.Kill("Aragorn"));
      bot.Answers(BrightRoomConversations.AttemptingToKillAragornAnswer());
      await bot.Receives(Intents.StartGame());
      bot.Answers(BrightRoomConversations.EntryDescription());
    }
  }

}
