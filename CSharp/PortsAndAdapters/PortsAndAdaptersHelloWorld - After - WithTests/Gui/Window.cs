using DataAccess.Ports;

namespace Gui
{
    public class Window
    {
      readonly IAppLogic _logic;

      public Window(IAppLogic appLogic)
      {
        _logic = appLogic;
      }

      public void OnSubmitClicked()
      {
        _logic.HandleAddEmployeeRequest();
      }
    }
}
