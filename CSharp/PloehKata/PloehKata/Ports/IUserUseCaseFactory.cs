namespace PloehKata.Ports
{
  public interface IUserUseCaseFactory
  {
    IUserUseCase CreateConnectionUseCase(string user1Id, string user2Id, IConnectionInProgress connectionInProgress);
  }
}