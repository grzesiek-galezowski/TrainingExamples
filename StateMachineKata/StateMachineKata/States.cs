namespace StateMachineKata
{
  public class States
  {
    public States(IMp3PlayerState playing, IMp3PlayerState initial, IMp3PlayerState paused)
    {
      Playing = playing;
      Initial = initial;
      Paused = paused;
    }

    public IMp3PlayerState Playing { get; private set; }
    public IMp3PlayerState Initial { get; private set; }
    public IMp3PlayerState Paused { get; private set; }
  }
}