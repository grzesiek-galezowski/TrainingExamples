using DataAccess.Ports;

namespace ApplicationLogic
{
  public class AppLogic : IAppLogic
  {
    readonly IPersistentStorage _storage;

    public AppLogic(IPersistentStorage persistentStorage)
    {
      _storage = persistentStorage;
    }

    public void HandleAddEmployeeRequest()
    {
      _storage.SaveEmployee();
    }
  }
}
