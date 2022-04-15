using MockNoMock.UsersApp.Adapter.UserApi;

namespace MockNoMock;

public interface IUserApi
{
  Task CreateNewUser(UserDto userDto);
}