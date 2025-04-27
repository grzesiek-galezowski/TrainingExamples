using ApplicationLogic.Ports;
using Lib;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;
/*
namespace EndToEndSpecification
{
  public class UserDtoBuilder
  {
    private readonly int _age;
    private readonly string _login;
    private readonly string _password;

    private UserDtoBuilder(int age, string login, string password)
    {
      _age = age;
      _login = login;
      _password = password;
    }

    public UserDto Build()
    {
      return new UserDto
      {
        Age = _age,
        Login = _login,
        Password = _password
      };
    }

    public UserDtoBuilder OfAge(int age)
    {
      return new UserDtoBuilder(age, _login, _password);
    }

    public static UserDtoBuilder AUser()
    {
      return new UserDtoBuilder(
        Any.Integer(), 
        Any.Login(),
        Any.Password()
      );
    }

    public UserDtoBuilder Adult()
    {
      return OfAge(18);
    }
  }
}*/