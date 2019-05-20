namespace DotNetJunkieKataWithCommandFactory
{
  public interface ICommandFactory
  {
    ICommand CreateCommand(int customerId, Address newAddress);
  }
}