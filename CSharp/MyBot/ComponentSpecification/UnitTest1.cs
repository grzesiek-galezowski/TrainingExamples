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
      bot.Answers(BotPhrases.EntryDescription());
      await bot.Receives(Intents.Kill("Gandalf"));
      bot.Answers(BotPhrases.AttemptingToKillGandalfAnswer());
      await bot.Receives(Intents.StartGame());
      bot.Answers(BotPhrases.EntryDescription());
    }

    [Fact]
    public async Task ShouldAllowTryingToKillAragornAndEndGame()
    {
      var bot = new AppDriver();
      await bot.Receives(Intents.StartGame());
      bot.Answers(BotPhrases.EntryDescription());
      await bot.Receives(Intents.Kill("Aragorn"));
      bot.Answers(BotPhrases.AttemptingToKillAragornAnswer());
      await bot.Receives(Intents.StartGame());
      bot.Answers(BotPhrases.EntryDescription());
    }

    [Fact]
    public async Task ShouldAllowTalkingToAragornAndGivingFianceeName()
    {
      var bot = new AppDriver();
      await bot.Receives(Intents.StartGame());
      bot.Answers(BotPhrases.EntryDescription());
      await bot.Receives(Intents.TalkTo("Aragorn"));
      bot.Answers(BotPhrases.AragornAsksAboutFianceeName());
      await bot.Receives(Intents.Words("Mandaryna Mandrykiewicz"));
      bot.Answers(
        BotPhrases.AragornTellsTheStoryOfHisFianceeAfterAcknowleding("Mandaryna Mandrykiewicz")
        + BotPhrases.EntryDescription());
      
    }
  }

}
