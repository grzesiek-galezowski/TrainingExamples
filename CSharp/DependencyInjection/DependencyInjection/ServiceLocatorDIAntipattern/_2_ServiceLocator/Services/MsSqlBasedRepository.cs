using ServiceLocatorDIAntipattern._2_ServiceLocator.Interfaces;
using Unity;

namespace ServiceLocatorDIAntipattern._2_ServiceLocator.Services
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