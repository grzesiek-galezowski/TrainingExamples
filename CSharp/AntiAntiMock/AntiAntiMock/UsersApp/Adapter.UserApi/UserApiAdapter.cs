namespace MockNoMock.UsersApp.Adapter.UserApi;

public class UserApiAdapter
{
  public UserApiAdapter(IUserApiSupport support, string baseUrl)
  {
    UserApi = new LogScopedHttpUserApi(
      support, 
      new HttpBasedUserApi(
        support, 
        new RequestFactory(baseUrl)));
  }

  public IUserApi UserApi { get; }
}