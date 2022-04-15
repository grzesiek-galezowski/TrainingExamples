using MockNoMock.UsersApp.Adapter.Controllers;
using MockNoMock.UsersApp.Adapter.Logging;
using MockNoMock.UsersApp.Adapter.UserApi;
using MockNoMock.UsersApp.AppLogic;

namespace MockNoMock.UsersApp;

public class CompositionRoot
{
  private readonly ControllersAdapter _controllersAdapter;

  public CompositionRoot(string configServiceBaseUrl)
  {
    var loggingAdapter = new LoggingAdapter();
    _controllersAdapter =
      new ControllersAdapter(
        new ApplicationLogic(
            new UserApiAdapter(
                loggingAdapter.UserApiSupport,
                configServiceBaseUrl)
              .UserApi)
          .CommandFactory);
  }

  public UserController NewUserController() => _controllersAdapter.NewUserController();
}