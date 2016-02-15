namespace DataAccess.Ports
{
  public interface IPersistentStorage
  {
    void SaveEmployee();
  }

  public class PersistentStorage2 : IPersistentStorage
  {
    public void SaveEmployee()
    {
    }
  }
}