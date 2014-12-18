using Microsoft.Practices.Unity;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Core;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Inbound;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Outbound;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Services;

namespace ServiceLocatorDIAntipattern._2_ServiceLocator
{
    public class ApplicationRoot
    {
      static ApplicationRoot()
      {
        Context.RegisterType<IProcessingWorkflow, AcmeProcessingWorkflow>(new ContainerControlledLifetimeManager());
        Context.RegisterType<IInbound, BinaryUdpInbound>(new ContainerControlledLifetimeManager());
        Context.RegisterType<IInputSocket, UdpSocket>(new ContainerControlledLifetimeManager());
        Context.RegisterType<IOutputSocket, TcpSocket>(new ContainerControlledLifetimeManager());
        Context.RegisterType<IPacketParsing, BinaryParsing>(new ContainerControlledLifetimeManager());
        Context.RegisterType<IOutbound, Outbound.Outbound>(new ContainerControlledLifetimeManager());
        Context.RegisterType<IAuthorization, ActiveDirectoryBasedAuthorization>(new ContainerControlledLifetimeManager());
        Context.RegisterType<IRepository, MsSqlBasedRepository>(new ContainerControlledLifetimeManager());
      }

      public void Main()
      {
        var sys = new TeleComSystem();
        sys.Start();
      }

      //simplified
      public static UnityContainer Context = new UnityContainer();
    }




}
