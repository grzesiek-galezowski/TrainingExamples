namespace StateMachineKata
{
  public interface IMp3PlayerContext
  {
    void GoTo(IMp3PlayerState mp3PlayerState);
    void SetCurrentTrack(string album, string track);
    void PlayCurrentTrack();
  }
}