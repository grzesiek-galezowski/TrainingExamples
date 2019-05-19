using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using SpecFlowExample.AutomationLayer.Actors;

namespace SpecFlowExample.AutomationLayer.Infrastructure
{
  // This is the "locator" (or maybe not? Maybe it's merely
  // a Registry (https://martinfowler.com/eaaCatalog/registry.html)?).
  // In this simplistic example, it uses concrete classes as its items, 
  // although in real-life scenarios, using interfaces here is sometimes warranted.
  public class ChatScenarioContext
  {
    private readonly List<User> _users = new List<User>();
    private readonly List<ChatRoom> _chatRooms = new List<ChatRoom>();

    public void Register(User user)
    {
      _users.Should().NotContain(user);
      _users.Add(user);
    }

    public User LocateUser(string userName)
    {
      return _users.Single(u => u.HasName(userName));
    }

    public ChatRoom LocateChatRoom(string chatRoomName)
    {
      return _chatRooms.Single(u => u.HasName(chatRoomName));
    }

    public void RemoveUsers()
    {
      foreach (var user in _users)
      {
        user.DeleteFromSystem();
      }
    }

    public void RemoveChatRooms()
    {
      foreach (var chatRoom in _chatRooms)
      {
        chatRoom.DeleteFromSystem();
      }
    }

    public void Register(ChatRoom chatRoom)
    {
      _chatRooms.Should().NotContain(chatRoom);
      _chatRooms.Add(chatRoom);
    }

    public ChatRoom ChatRoomInTheSpotlight()
    {
      return _chatRooms.Last();
    }
  }
}