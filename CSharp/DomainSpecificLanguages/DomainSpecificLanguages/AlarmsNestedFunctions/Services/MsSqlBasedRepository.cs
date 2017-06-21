using TelecomSystemNestedFunctions.Interfaces;

namespace TelecomSystemNestedFunctions.Services
{
  public interface IRepository
  {
    void Save(AcmeMessage message);
  }

  public class MsSqlBasedRepository : IRepository
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