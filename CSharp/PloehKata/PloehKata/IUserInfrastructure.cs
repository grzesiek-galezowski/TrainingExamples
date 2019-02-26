namespace PloehKata
{
  public interface IUserInfrastructure
  {
    IUserCommand CreateConnectionCommand(IConnectionInProgress connectionInProgress, string user1Id, string user2Id);
    IConnectionInProgress CreateConnectionInProgress();
  }
}