using Microsoft.AspNetCore.Mvc;

namespace PloehKata //http://blog.ploeh.dk/2019/02/25/an-example-of-interaction-based-testing-in-c/
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
      private readonly IUserInfrastructure _infrastructure;

      public ConnectionsController(IUserInfrastructure infrastructure)
      {
        _infrastructure = infrastructure;
      }

      [HttpPost]
        public IActionResult Connect(string id, string otherUserId)
        {
          var connectionInProgress = _infrastructure.CreateConnectionInProgress();
          var userCommand = _infrastructure.CreateConnectionCommand(connectionInProgress, id, otherUserId);
          userCommand.Execute();
          return connectionInProgress.ToActionResult();
        }


    }
}
