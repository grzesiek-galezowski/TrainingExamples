using System;
using ApplicationLogic.Ports;

namespace ApplicationLogic
{
  public interface IUserQuery
  {
    UserDto Execute();
  }

  public class GetUserByIdQuery : IUserQuery
  {
    private readonly string _id;
    private readonly IUsersRepository _usersRepository;

    public GetUserByIdQuery(IUsersRepository usersRepository, string id)
    {
      _usersRepository = usersRepository;
      _id = id;
    }

    public UserDto Execute()
    {
      return _usersRepository.GetUserBy(_id);
    }
  }
}