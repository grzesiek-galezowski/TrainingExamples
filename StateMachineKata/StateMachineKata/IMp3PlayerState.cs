namespace StateMachineKata
{
  public interface IMp3PlayerState
  {
    void Choose(string album, string track, IMp3PlayerContext mp3PlayerContext, States states);
    void Play(IMp3PlayerContext mp3PlayerContext, States states);
    void Pause(IMp3PlayerContext mp3PlayerContext, States states);
    void Stop(Mp3Player mp3Player, States states);
    void Enter(IMp3PlayerContext mp3PlayerContext);
  }
}