﻿using ApplicationLogic;

namespace PortsAndAdaptersHelloWorld
{
    public class Window
    {
      private readonly IAppLogic _logic;

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
