using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Domain
{
    public class DomainLogic
    {
      private readonly DatabaseObject _database = new DatabaseObject();

      public void HandleAddEmployeeRequest()
      {
        _database.SaveEmployee();
      }
    }
}
