using DataAccess.Ports;
using DataAccess.Ports.Primary;
using DataAccess.Ports.Secondary;

namespace Domain
{
    public class DomainLogic : IDomainLogic
    {
      private readonly IPersistentStorage _storage;

      public DomainLogic(
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
