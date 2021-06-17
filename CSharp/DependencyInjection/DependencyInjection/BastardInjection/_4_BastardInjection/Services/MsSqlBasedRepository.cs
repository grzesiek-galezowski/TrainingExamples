using BastardInjection._4_BastardInjection.Interfaces;

namespace BastardInjection._4_BastardInjection.Services
{
  public interface IRepository
  {
    void Save(AcmeMessage message);
  }

  public class MsSqlBasedRepository : IRepository
  {
    private readonly DataDestination _dataDestination;

    public MsSqlBasedRepository() : this(new SqlDataDestination())
    {
      
    }

    //for tests
    public MsSqlBasedRepository(DataDestination dataDestination)
    {
      _dataDestination = dataDestination;
    }

    public void Save(AcmeMessage message)
    {
      message.WriteTo(_dataDestination);
    }
  }
}