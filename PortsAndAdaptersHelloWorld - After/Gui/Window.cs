using System.Security.Cryptography;
using DataAccess.Ports;

namespace Gui
{
    public class Window
    {
      readonly IDomainLogic _logic;

      public Window(IDomainLogic domainLogic)
      {
        _logic = domainLogic;
      }

      public void OnSubmitClicked()
      {
        _logic.HandleAddEmployeeRequest();
      }
    }
}
