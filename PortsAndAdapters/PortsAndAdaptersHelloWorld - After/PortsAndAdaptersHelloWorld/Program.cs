using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Domain;
using Gui;

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
