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

  public interface IMp3Player
  {
    void Choose(string album, string track);
    void Play();
    void Pause();
    void Stop();
  }

  internal interface Mp3PlayerContext
  {
  }


  public class Mp3Player : IMp3Player, Mp3PlayerContext
  {
    Mp3PlayerState _currentState = new Mp3PlayerState();
    
    public void Choose(string album, string track)
    {
      _currentState.Choose(album, track, this);
    }

    public void Play()
    {
      _currentState.Play(this);
    }

    public void Pause()
    {
      _currentState.Pause(this);
    }

    public void Stop()
    {
      throw new System.NotImplementedException();
    }
  }

  internal class Mp3PlayerState
  {
    public void Play(Mp3PlayerContext mp3Player)
    {
      throw new System.NotImplementedException();
    }

    public void Pause(Mp3PlayerContext mp3PlayerContext)
    {
      throw new System.NotImplementedException();
    }

    public void Choose(string album, string track, Mp3PlayerContext mp3PlayerContext)
    {
      throw new System.NotImplementedException();
    }
  }

}
