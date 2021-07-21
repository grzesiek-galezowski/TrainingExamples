using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Core;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Inbound;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Outbound;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Services;
using Unity;
using Unity.Lifetime;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection
{
    public class ApplicationRoot
    {

      public void Main_BareConstructors()
      {
        var sys = new TeleComSystem(
          new MessageInbound(
            new UdpSocket(), 
            new BinaryParsing()), 
          new MessageOutbound(
            new TcpSocket(), 
            new XmlOutboundMessageFactory()), 
          new AcmeProcessingWorkflow(
            new ActiveDirectoryBasedAuthorization(), 
            new MsSqlBasedRepository(
              new SqlDataDestination())));

        sys.Start();
      }

      public void Main_FluentInterfaceWay()
      {
        //Growing Object Oriented Software Guided By Tests
        //Building on SOLID Foundations
        var sys = ASystemThat(
          ReceivesMessages(
            ViaUdp(),
            InBinaryFormat()),
          AndThen(
            AuthenticatesThemViaActiveDirectory(),
            PersistsInSqlDatabase()),
          AndSendsThem(ViaTcp(), AsXml()));

        sys.Start();
      }

      public void Main_IoC_Container()
      {
        using (var container = new UnityContainer())
        {
          //Register
          container
            .RegisterType<DataDestination, SqlDataDestination>(
              new ContainerControlledLifetimeManager()
            )
            .RegisterType<IRepository, MsSqlBasedRepository>()
            .RegisterType<IAuthorization, ActiveDirectoryBasedAuthorization>()
            .RegisterType<IAcmeProcessingWorkflow, AcmeProcessingWorkflow>()
            .RegisterType<IOutboundMessageFactory, XmlOutboundMessageFactory>()
            .RegisterType<ISocket, TcpSocket>()
            .RegisterType<IOutbound, MessageOutbound>()
            .RegisterType<IParsing, BinaryParsing>()
            .RegisterType<IInboundSocket, UdpSocket>()
            .RegisterType<IInbound, MessageInbound>()
            .RegisterType<TeleComSystem>();

          //////// Only one Resolve() call after this line! ////////////

          //Resolve
          var system = container.Resolve<TeleComSystem>();

          system.Start();

        } //Release
      }


      private static MsSqlBasedRepository PersistsInSqlDatabase()
      {
        return new MsSqlBasedRepository(
          new SqlDataDestination());
      }

      private static ActiveDirectoryBasedAuthorization AuthenticatesThemViaActiveDirectory()
      {
        return new ActiveDirectoryBasedAuthorization();
      }

      private AcmeProcessingWorkflow AndThen(ActiveDirectoryBasedAuthorization activeDirectoryBasedAuthorization, MsSqlBasedRepository msSqlBasedRepository)
      {
        return new AcmeProcessingWorkflow(activeDirectoryBasedAuthorization, msSqlBasedRepository);
      }

      private static XmlOutboundMessageFactory AsXml()
      {
        return new XmlOutboundMessageFactory();
      }

      private static TcpSocket ViaTcp()
      {
        return new TcpSocket();
      }

      private MessageOutbound AndSendsThem(TcpSocket tcpSocket, XmlOutboundMessageFactory xmlOutboundMessageFactory)
      {
        return new MessageOutbound(tcpSocket, xmlOutboundMessageFactory);
      }

      private static BinaryParsing InBinaryFormat()
      {
        return new BinaryParsing();
      }

      private static UdpSocket ViaUdp()
      {
        return new UdpSocket();
      }

      private MessageInbound ReceivesMessages(UdpSocket udpSocket, BinaryParsing binaryParsing)
      {
        return new MessageInbound(udpSocket, binaryParsing);
      }

      private TeleComSystem ASystemThat(MessageInbound messageInbound, AcmeProcessingWorkflow acmeProcessingWorkflow, MessageOutbound messageOutbound)
      {
        return new TeleComSystem(messageInbound, messageOutbound, acmeProcessingWorkflow);
      }


    }

}
