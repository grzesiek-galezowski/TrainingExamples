using System;

namespace PloehKata
{
  public class ConnectionCommand : IUserCommand
  {
    private readonly IConnectionInProgress _connectionInProgress;
    private readonly string _user1Id;
    private readonly string _user2Id;
    private readonly IUserRepository _repository;

    public ConnectionCommand(
      IConnectionInProgress connectionInProgress, 
      string user1Id, 
      string user2Id,
      IUserRepository repository)
    {
      _connectionInProgress = connectionInProgress;
      _user1Id = user1Id;
      _user2Id = user2Id;
      _repository = repository;
    }

    public void Execute()
    {
      //bug split into two interfaces
      var connector = _repository.LookupConnector(_user1Id);
      var connectee = _repository.LookupConnectee(_user2Id);
      connector.AttemptConnectionWith(connectee, _connectionInProgress);
      connector.WriteTo(_repository);
    }
  }
}