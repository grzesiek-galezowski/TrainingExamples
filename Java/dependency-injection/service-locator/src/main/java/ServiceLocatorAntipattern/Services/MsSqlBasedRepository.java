  public interface IRepository
  {
    void Save(AcmeMessage message);
  }

  public class MsSqlBasedRepository : IRepository
  {
    private final DataDestination _sqlDataDestination = ApplicationRoot.Context.Resolve<SqlDataDestination>();

    public void Save(AcmeMessage message)
    {
      message.WriteTo(_sqlDataDestination);
    }
  }
