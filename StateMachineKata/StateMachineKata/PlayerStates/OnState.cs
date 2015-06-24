namespace StateMachineKata.PlayerStates
{
  internal class OnState : IMp3PlayerState
  {
    public void Play(IMp3PlayerContext mp3Player, States states)
    {
      mp3Player.GoTo(states.Playing);
    }

    public void Pause(IMp3PlayerContext mp3PlayerContext, States states)
    {
      //do nothing - nothing to pause
    }

    public void Stop(Mp3Player mp3Player, States states)
    {
      //do nothing - nothing to stop
    }

    public void Enter(IMp3PlayerContext mp3PlayerContext)
    {
      //bug display track list?
      throw new System.NotImplementedException();
    }

    public void Choose(string album, string track, IMp3PlayerContext mp3PlayerContext, States states)
    {
      mp3PlayerContext.SetCurrentTrack(album, track);
    }
  }
}