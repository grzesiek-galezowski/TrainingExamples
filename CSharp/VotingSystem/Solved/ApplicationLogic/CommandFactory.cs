using System;
using ApplicationLogic.Ports;

namespace ApplicationLogic
{
  public interface ICommandFactory
  {
    IRegisterUserCommand CreateRegisterUserCommand(
      UserDto userDto, 
      IResultBuilder resultBuilder);

    IUserQuery CreateGetUserByIdQuery(string id);
  }

  public class CommandFactory : ICommandFactory
  {
    private readonly IUsersRepository _usersRepository;

    public CommandFactory(IUsersRepository usersRepository)
    {
      _usersRepository = usersRepository;
    }

    public IRegisterUserCommand CreateRegisterUserCommand(
      UserDto userDto, 
      IResultBuilder resultBuilder)
    {
      return new RegisterUserCommand(
        _usersRepository, 
        resultBuilder, 
        new UserUnderRegistration(userDto));
    }

    public IUserQuery CreateGetUserByIdQuery(string id)
    {
      return new GetUserByIdQuery(_usersRepository, id);
    }
  }
}