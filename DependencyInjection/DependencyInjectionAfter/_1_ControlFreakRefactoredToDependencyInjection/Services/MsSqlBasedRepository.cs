using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Services
{
  internal interface IRepository
  {
    void Save(AcmeMessage message);
  }

  class MsSqlBasedRepository : IRepository
  {
    private readonly DataDestination _sqlDataDestination;

    public MsSqlBasedRepository(DataDestination sqlDataDestination)
    {
      _sqlDataDestination = sqlDataDestination;
    }

    public void Save(AcmeMessage message)
    {
      message.WriteTo(_sqlDataDestination);
    }
  }
}