using System.Threading.Tasks;
using BotLogic;
using ComponentSpecification.Automation;
using Xunit;
using static BotLogic.BotPhrases;
using static ComponentSpecification.Automation.Intents;

namespace ComponentSpecification
{
  public class BrightRoomConversationSpecification
  {
    [Fact]
    public async Task ShouldAllowTryingToKillGandalfAndEndGame()
    {
      var bot = new AppDriver();
      await bot.Receives(StartGame());
      bot.AnswersWith(EntryDescription());
      await bot.Receives(Kill("Gandalf"));
      bot.AnswersWith(AttemptingToKillGandalfAnswer());
      await bot.Receives(StartGame());
      bot.AnswersWith(EntryDescription());
    }

    [Fact]
    public async Task ShouldAllowTryingToKillAragornAndEndGame()
    {
      var bot = new AppDriver();
      await bot.Receives(StartGame());
      bot.AnswersWith(EntryDescription());
      await bot.Receives(Kill("Aragorn"));
      bot.AnswersWith(AttemptingToKillAragornAnswer());
      await bot.Receives(StartGame());
      bot.AnswersWith(EntryDescription());
    }

    [Fact]
    public async Task ShouldAllowTalkingToAragornAndGivingFianceeName()
    {
      var bot = new AppDriver();
      await bot.Receives(StartGame());
      bot.AnswersWith(EntryDescription());
      await bot.Receives(TalkTo("Aragorn"));
      bot.AnswersWith(QuestionFromAragornAboutFrodosFianceeName());
      await bot.Receives(Words("Mandaryna Mandrykiewicz"));
      bot.AnswersWith(
        AragornsStoryOfHisFianceeAfterAcknowleding("Mandaryna Mandrykiewicz"),
        EntryDescription());
    }

    [Fact]
    public async Task ShouldMakeAragornJokeWhenFianceeNameIsAragorn()
    {
      var bot = new AppDriver();
      await bot.Receives(StartGame());
      bot.AnswersWith(EntryDescription());
      await bot.Receives(TalkTo("Aragorn"));
      bot.AnswersWith(QuestionFromAragornAboutFrodosFianceeName());
      await bot.Receives(Words("Aragorn"));
      bot.AnswersWith(
        AragornJokesAboutHimBeingAFianceeOfFrodo(),
        QuestionFromAragornAboutFrodosFianceeName());
    }
    
    [Fact]
    public async Task ShouldMakeAragornClarifyHisFianceeQuestionWhenAskedForDetails()
    {
      var bot = new AppDriver();
      await bot.Receives(StartGame());
      bot.AnswersWith(EntryDescription());
      await bot.Receives(TalkTo("Aragorn"));
      bot.AnswersWith(QuestionFromAragornAboutFrodosFianceeName());
      await bot.Receives(QuestionWho());
      bot.AnswersWith(ClarificationFromAragornAboutFrodosFianceeName());
      await bot.Receives(Words("Mandaryna Mandrykiewicz"));
      bot.AnswersWith(
        AragornsStoryOfHisFianceeAfterAcknowleding("Mandaryna Mandrykiewicz"),
        EntryDescription());
    }

  }

}
