using System.Diagnostics;
using StateMachineKata.PlayerStates;

namespace StateMachineKata
{
  //https://developers.google.com/cast/docs/custom_receiver
  //https://developers.google.com/cast/images/receiver_state_machine.png
    
  public class CompositionRoot
  {
    public void Main()
    {
      var boomBox = new Mp3Player(new MusicStorage(), new States(new PlayingState(), new OnState(), new PausedState()));
      WorkWith(boomBox);
    }

    private void WorkWith(IMp3Player mp3Player)
    {
      mp3Player.Choose("The Prodigy - The Fat Of The Land", "02 - Breathe");
      mp3Player.Play();
      mp3Player.Pause();
      mp3Player.Play();
      mp3Player.Stop();
      mp3Player.Play();
    }
  }
}
