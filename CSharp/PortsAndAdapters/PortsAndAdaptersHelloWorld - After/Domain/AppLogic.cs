using DataAccess.Ports.Primary;
using DataAccess.Ports.Secondary;

namespace ApplicationLogic
{
    public class AppLogic : IAppLogic
    {
      private readonly IPersistentStorage _storage;

      public AppLogic(
        IPersistentStorage persistentStorage)
      {
        _storage = persistentStorage;
      }

      public void HandleAddEmployeeRequest()
      {
        _storage.SaveEmployee();
      }
    }
}
