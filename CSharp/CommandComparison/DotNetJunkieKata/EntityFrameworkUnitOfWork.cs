namespace DotNetJunkieKata
{
  internal class EntityFrameworkUnitOfWork : UnitOfWork
  {
    private readonly string _connectionString;

    public EntityFrameworkUnitOfWork(string connectionString)
    {
      _connectionString = connectionString;
    }
  }
}