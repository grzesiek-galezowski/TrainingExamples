namespace StateMachineKata
{
  public interface IMp3Player
  {
    void Choose(string album, string track);
    void Play();
    void Pause();
    void Stop();
  }
}