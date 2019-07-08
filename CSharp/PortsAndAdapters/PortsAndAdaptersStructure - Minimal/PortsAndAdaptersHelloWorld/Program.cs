using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace PortsAndAdaptersHelloWorld
{
  static class Program
  {
    static void Main(string[] args)
    {
      var window = new Window(
        new DomainLogic(
          new DatabaseObject()));

      window.OnSubmitClicked();
    }
  }
}
