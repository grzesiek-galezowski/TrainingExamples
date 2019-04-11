namespace BotLogic
{
  public static class BotPhrases
  {
    public static string EntryDescription()
    {
      return "Frdo, you are in a bright room. On your left stands Gandalf. He seems to be watching you. Aragorn is standing in front of you.";
    }

    public static string AttemptingToKillGandalfAnswer()
    {
      return "You jump at Gandalf with the intent of suffocating him, " +
             "but he quickly sees through your charade and strikes you with a fireball. " +
             "You are now but a pile of ash. - Oh well - says Gandalf to Aragorn - " +
             "I told you from the get-go that using the eagles would be a better idea...";
    }

    public static string AttemptingToKillAragornAnswer()
    {
      return "You attack Aragorn with a big stone just for fun to check if Aragorn can " +
             "truly deflect any hit with his sword. Apparently, his muscle memory worked a bit too well." +
             " You could see it very clearly from the right distance as your head, cut off from your corpse, " +
             "went flying through the window";

    }

    public static string QuestionFromAragornAboutFrodosFianceeName()
    {
      return
        $"Hey, Frodo, why are you so sad? As if you were yearning to one again see.. ekhem, well... what was her name?";
    }

    public static string AragornsStoryOfHisFianceeAfterAcknowleding(string frodosFianceeName)
    {
      return
        $"Aye, {frodosFianceeName} it is - " +
        $"I once knew her sister. We we together in the kindergarten when " +
        $"she suddenly sticked a fork in my head. " +
        $"That fateful day killed our love forever...";
    }

    public static string AragornJokesAboutHimBeingAFianceeOfFrodo()
    {
      return nameof(AragornJokesAboutHimBeingAFianceeOfFrodo);
    }

    public static string ClarificationFromAragornAboutFrodosFianceeName()
    {
      return "Thy fiancee name, blockhead!";
    }
  }
}