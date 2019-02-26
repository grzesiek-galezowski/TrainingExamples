using System;

namespace PloehKata
{
  public class UserInfrastructure : IUserInfrastructure
  {
    private IUserRepository _repository;

    public UserInfrastructure(IUserRepository repository)
    {
      _repository = repository;
    }

    public IUserCommand CreateConnectionCommand(
      IConnectionInProgress connectionInProgress, string user1Id, string user2Id)
    {
      return new ConnectionCommand(connectionInProgress, user1Id, user2Id, _repository);
    }

    public IConnectionInProgress CreateConnectionInProgress()
    {
      return new ConnectionInProgress();
    }
  }
}