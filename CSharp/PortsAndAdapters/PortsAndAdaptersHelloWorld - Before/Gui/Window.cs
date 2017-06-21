using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Gui
{
    public class Window
    {
      private readonly DomainLogic _logic = new DomainLogic();

      public void OnSubmitClicked()
      {
        _logic.HandleAddEmployeeRequest();
      }
    }
}
