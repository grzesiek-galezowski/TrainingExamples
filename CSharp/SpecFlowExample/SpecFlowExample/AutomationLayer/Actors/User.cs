using System;
using System.Collections.Generic;
using SpecFlowExample.AutomationLayer.Data;

namespace SpecFlowExample.AutomationLayer.Actors
{
  public class User : IEquatable<User>
  {
    public User(string name)
    {
      _name = name;
    }

    public bool Equals(User other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_name, other._name);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((User) obj);
    }

    public override int GetHashCode()
    {
      return (_name != null ? _name.GetHashCode() : 0);
    }

    public static bool operator ==(User left, User right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(User left, User right)
    {
      return !Equals(left, right);
    }

    private readonly string _name;

    public bool HasName(string userName)
    {
      return _name == userName;
    }

    public void AddFriend(User otherUser)
    {
      //a call to production code API
    }

    public void SendDirectMessage(User recipient, string messageText)
    {
      //a call to production code API
    }

    public void DeleteFromSystem()
    {
      //a call to production code API
    }

    public void RegisterInSystem()
    {
      //a call to production code API
    }

    public void ShouldSee(IEnumerable<DisplayedMessage> expectedMessages)
    {
      //an assertion 
    }

    public void SendDirectMessage(ChatRoom recipient, string messageText)
    {
      //a call to production code API
    }
  }
}