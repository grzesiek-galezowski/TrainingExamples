using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogic;

namespace PortsAndAdaptersHelloWorld
{
  static class Program
  {
    static void Main(string[] args)
    {
      var window = new Window(
        new AppLogic(
          new DatabaseObject()));

      window.OnSubmitClicked();
    }
  }
}
