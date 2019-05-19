using System;

namespace SpecFlowExample.AutomationLayer.Actors
{
  public class ChatRoom : IEquatable<ChatRoom>
  {
    private readonly string _chatRoomName;

    public ChatRoom(string chatRoomName)
    {
      _chatRoomName = chatRoomName;
    }

    public bool Equals(ChatRoom other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_chatRoomName, other._chatRoomName);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ChatRoom) obj);
    }

    public override int GetHashCode()
    {
      return (_chatRoomName != null ? _chatRoomName.GetHashCode() : 0);
    }

    public static bool operator ==(ChatRoom left, ChatRoom right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(ChatRoom left, ChatRoom right)
    {
      return !Equals(left, right);
    }

    public void Add(User user)
    {
      //call production code API
    }

    public void ShouldNotContainAnyMessagesFor(string userName)
    {
      //some kind of assertion code
    }

    public void DeleteFromSystem()
    {
      //call production code API
    }

    public bool HasName(string chatRoomName)
    {
      return _chatRoomName == chatRoomName;
    }
  }
}