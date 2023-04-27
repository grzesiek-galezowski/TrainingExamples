using Database;

namespace ApplicationLogic
{
    public class AppLogic
    {
      private readonly DatabaseObject _database = new DatabaseObject();

      public void HandleAddEmployeeRequest()
      {
        _database.SaveEmployee();
      }
    }
}
