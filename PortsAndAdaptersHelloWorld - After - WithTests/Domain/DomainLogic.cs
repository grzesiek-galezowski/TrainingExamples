using DataAccess.Ports;

namespace Domain
{
    public class DomainLogic : IDomainLogic
    {
      readonly IPersistentStorage _storage;

      public DomainLogic(IPersistentStorage persistentStorage)
      {
        _storage = persistentStorage;
      }

      public void HandleAddEmployeeRequest()
      {
        _storage.SaveEmployee();
      }
    }
}
