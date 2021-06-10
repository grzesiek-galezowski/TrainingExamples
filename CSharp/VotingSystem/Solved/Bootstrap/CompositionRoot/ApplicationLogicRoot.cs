using ApplicationLogic;
using ApplicationLogic.Ports;

namespace Bootstrap.CompositionRoot
{
  public class ApplicationLogicRoot
  {
    private readonly CommandFactory _commandFactory;

    public ApplicationLogicRoot(IUsersRepository repository)
    {
      _commandFactory = new CommandFactory(repository);
    }

    public CommandFactory CommandFactory()
    {
      return _commandFactory;
    }
  }
}