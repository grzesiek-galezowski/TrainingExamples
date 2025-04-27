using ApplicationLogic.Ports;

namespace ApplicationLogic
{
  public class RegisterUserCommand : IRegisterUserCommand
  {
    private readonly IResultBuilder _resultBuilder;
    private readonly IUsersRepository _usersRepository;
    private readonly IUserUnderRegistration _userUnderRegistration;

    public RegisterUserCommand(
      IUsersRepository usersRepository, 
      IResultBuilder resultBuilder, 
      IUserUnderRegistration userUnderRegistration)
    {
      _usersRepository = usersRepository;
      _userUnderRegistration = userUnderRegistration;
      _resultBuilder = resultBuilder;
    }

    public void Execute()
    {
      _userUnderRegistration.SaveIn(_usersRepository, _resultBuilder);
    }
  }
}