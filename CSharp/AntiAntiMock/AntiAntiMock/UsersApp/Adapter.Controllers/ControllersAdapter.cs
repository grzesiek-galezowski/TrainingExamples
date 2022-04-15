using MockNoMock.UsersApp.AppLogic;

namespace MockNoMock.UsersApp.Adapter.Controllers;

public class ControllersAdapter
{
  private readonly ICommandFactory _commandFactory;

  public ControllersAdapter(ICommandFactory commandFactory)
  {
    _commandFactory = commandFactory;
  }

  public UserController NewUserController() => new UserController(_commandFactory);
}