using ConformingContainerAntipattern._3_ConformingContainer.Interfaces;

namespace ConformingContainerAntipattern._3_ConformingContainer.Services
{
  public interface IRepository
  {
    void Save(AcmeMessage message);
  }

  public class MsSqlBasedRepository : IRepository
  {
    private readonly DataDestination _sqlDataDestination = ApplicationRoot.Context.Resolve<SqlDataDestination>();

    public void Save(AcmeMessage message)
    {
      message.WriteTo(_sqlDataDestination);
    }
  }
}