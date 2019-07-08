using Domain;

namespace PortsAndAdaptersHelloWorld
{
    public class Window
    {
      private readonly IDomainLogic _logic;

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
