using FrodoEntersTheRoom;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace FrodoEntersTheRoomSpecification
{
  class KillCharacterIntentSpecification
  {
    [Test]
    public void ShouldTranslateTheIntentToDialogSignalWithCharacterName()
    {
      var character = Any.String();
      var responsePhrase = Any.Instance<IResponsePhrase>();
      var dialog = Substitute.For<IDialog>();
      var killCharacterIntent = new KillCharacterIntent(character, dialog, responsePhrase);

      killCharacterIntent.Apply();

      dialog.Received(1).OnAttemptToKill(character, responsePhrase);
    }

  }
}