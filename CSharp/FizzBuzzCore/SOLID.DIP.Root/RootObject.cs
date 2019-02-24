using System;
using SOLID.DIP.Domain;

namespace SOLID.DIP.Root
{
    class RootObject
    {
        static void Main(string[] args)
        {
            var domainLogic = new DomainLogic();
            domainLogic.PerformCalculation(1,2);
        }
    }
}
