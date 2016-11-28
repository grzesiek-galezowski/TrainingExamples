using System;
using Microsoft.Practices.Unity;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Core;
using ServiceLocatorDIAntipattern._2_ServiceLocator.InMessages;
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
        // forgot about this... Context.RegisterType<IMarshalling, XmlMarshalling>(new ContainerControlledLifetimeManager());

        Context.RegisterType<SqlDataDestination>(new ContainerControlledLifetimeManager());
        
        //not container controlled
        Context.RegisterType<IOutboundMessage, OutboundMessage>();
        Context.RegisterType<Random>(new InjectionFactory(container => new Random()));
        Context.RegisterType<NullMessage>();
        Context.RegisterType<StartMessage>();
        Context.RegisterType<StopMessage>();
      }

      public static void Main(string[] args)
      {
        try
        {
          var sys = new TeleComSystem(); //uses container inside, but should be resolved itself!
          sys.Start();

        }
        finally
        {
          Context.Dispose();
        }
      }

      //simplified
      public static final UnityContainer Context = new UnityContainer();
    }




}
