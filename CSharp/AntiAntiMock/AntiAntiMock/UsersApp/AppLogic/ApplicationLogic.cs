namespace MockNoMock.UsersApp.AppLogic;

public class ApplicationLogic
{
  public ApplicationLogic(IUserApi userApi)
  {
    CommandFactory = new CommandFactory(userApi);
  }

  public ICommandFactory CommandFactory { get; }
}