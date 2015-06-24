namespace StateMachineKata.PlayerStates
{
  public class PlayingState : IMp3PlayerState
  {
    public void Choose(string album, string track, IMp3PlayerContext mp3PlayerContext, States states)
    {
      throw new System.NotImplementedException();
    }

    public void Play(IMp3PlayerContext mp3PlayerContext, States states)
    {
      throw new System.NotImplementedException();
    }

    public void Pause(IMp3PlayerContext mp3PlayerContext, States states)
    {
      mp3PlayerContext.GoTo(states.Paused);
    }

    public void Stop(Mp3Player mp3Player, States states)
    {
      throw new System.NotImplementedException();
    }

    public void Enter(IMp3PlayerContext mp3PlayerContext)
    {
      mp3PlayerContext.PlayCurrentTrack();
    }
  }
}