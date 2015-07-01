using System;
using NSubstitute;
using NUnit.Framework;
using StateMachineKata.PlayerStates;

namespace StateMachineKata
{
  public class Mp3Player : IMp3Player, IMp3PlayerContext
  {
    IMp3PlayerState _currentState;
    readonly States _states;
    private IMusicTrack _currentTrack = new NoMusicTrack();
    private readonly MusicStorage _musicStorage;

    public Mp3Player(MusicStorage musicStorage, States states)
    {
      _musicStorage = musicStorage;
      _states = states;
      _currentState = _states.Initial;
    }

    public void Choose(string album, string track)
    {
      _currentState.Choose(album, track, this, _states);
    }

    public void Play()
    {
      _currentState.Play(this, _states);
    }

    public void Pause()
    {
      _currentState.Pause(this, _states);
    }

    public void Stop()
    {
      _currentState.Stop(this, _states);
    }

    public void GoTo(IMp3PlayerState mp3PlayerState)
    {
      _currentState = mp3PlayerState;
      _currentState.Enter(this);
    }

    public void SetCurrentTrack(string album, string track)
    {
      _currentTrack = _musicStorage.RetrieveTrack(album, track);
    }

    public void PlayCurrentTrack()
    {
      _currentTrack.Play();
    }
  }

}