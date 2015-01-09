using DependencyInjectionBefore._1_ControlFreak.Interfaces;

namespace DependencyInjectionBefore._1_ControlFreak.Services
{
  class MsSqlBasedRepository
  {
    private readonly DataDestination _sqlDataDestination = new SqlDataDestination();

    public void Save(AcmeMessage message)
    {
      message.WriteTo(_sqlDataDestination);
    }
  }
}