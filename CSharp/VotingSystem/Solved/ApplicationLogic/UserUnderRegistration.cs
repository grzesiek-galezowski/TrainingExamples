using ApplicationLogic.Ports;

namespace ApplicationLogic
{
  public interface IUserUnderRegistration
  {
    void SaveIn(IUsersRepository usersRepository, IResultBuilder resultBuilder);
  }


  public class UserUnderRegistration : IUserUnderRegistration
  {
    private readonly UserDto _userDto;

    public UserUnderRegistration(UserDto userDto)
    {
      _userDto = userDto;
    }

    public void SaveIn(IUsersRepository usersRepository, IResultBuilder resultBuilder)
    {
      usersRepository.Add(_userDto);
      resultBuilder.UserAddedSuccessfully(_userDto);
    }
  }
}