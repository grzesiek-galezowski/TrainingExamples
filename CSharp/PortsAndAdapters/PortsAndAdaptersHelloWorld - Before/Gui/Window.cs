using ApplicationLogic;

namespace Gui
{
    public class Window
    {
      private readonly AppLogic _logic = new AppLogic();

      public void OnSubmitClicked()
      {
        _logic.HandleAddEmployeeRequest();
      }
    }
}
