using Microsoft.AspNetCore.Mvc;
using PloehKata.Ports;

namespace PloehKata.Adapters //http://blog.ploeh.dk/2019/02/25/an-example-of-interaction-based-testing-in-c/
{
  [Route("api/[controller]")]
  [ApiController]
  public class ConnectionsController : ControllerBase
  {
    private readonly IUserUseCaseFactory _useCaseFactory;
    private readonly IConnectionInProgressFactory _connectionInProgressFactory;

    public ConnectionsController(
      IUserUseCaseFactory useCaseFactory, 
      IConnectionInProgressFactory connectionInProgressFactory)
    {
      _useCaseFactory = useCaseFactory;
      _connectionInProgressFactory = connectionInProgressFactory;
    }

    [HttpPost]
    public IActionResult Connect(string id, string otherUserId)
    {
      var connectionInProgress = _connectionInProgressFactory.CreateConnectionInProgress();
      var userCommand = _useCaseFactory.CreateConnectionUseCase(id, otherUserId, connectionInProgress);
      userCommand.Execute();
      return connectionInProgress.ToActionResult();
    }
  }
}