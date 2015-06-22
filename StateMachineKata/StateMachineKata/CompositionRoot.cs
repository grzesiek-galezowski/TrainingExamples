using System.Diagnostics;

namespace StateMachineKata
{
  //https://developers.google.com/cast/docs/custom_receiver
  //https://developers.google.com/cast/images/receiver_state_machine.png
    
  public class CompositionRoot
  {
    public void Main()
    {
      var boomBox = new Mp3Player();
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

  internal class Casette : ICasette
  {
    public Casette(string title)
    {
      throw new System.NotImplementedException();
    }
  }

  public interface IMp3Player
  {
    void Choose(string album, string track);
    void Play();
    void Pause();
    void Stop();
  }

  public class Mp3Player : IMp3Player
  {
    public void Choose(string album, string track)
    {
      throw new System.NotImplementedException();
    }

    public void Play()
    {
      throw new System.NotImplementedException();
    }

    public void Pause()
    {
      throw new System.NotImplementedException();
    }

    public void Stop()
    {
      throw new System.NotImplementedException();
    }
  }
}
