using TelecomSystemNestedFunctions.Core;
using TelecomSystemNestedFunctions.Inbound;
using TelecomSystemNestedFunctions.Outbound;
using TelecomSystemNestedFunctions.Services;

namespace TelecomSystemNestedFunctions
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
        var sys = 
          TeleComSystem(
            InputConfiguration
            (
              InterfaceUdp(),
              FormatBinary()
            ),
            PreprocessingRuleChain
            (
              Authentication(),
              Persistence()
            ),
            OutputConfiguration
            (
              InterfaceTcp(), 
              FormatXml()
            )
        );

        sys.Start();
      }



      private static MsSqlBasedRepository Persistence()
      {
        return new MsSqlBasedRepository(
          new SqlDataDestination());
      }

      private static ActiveDirectoryBasedAuthorization Authentication()
      {
        return new ActiveDirectoryBasedAuthorization();
      }

      private AcmeProcessingWorkflow PreprocessingRuleChain(ActiveDirectoryBasedAuthorization activeDirectoryBasedAuthorization, MsSqlBasedRepository msSqlBasedRepository)
      {
        return new AcmeProcessingWorkflow(activeDirectoryBasedAuthorization, msSqlBasedRepository);
      }

      private static XmlOutboundMessageFactory FormatXml()
      {
        return new XmlOutboundMessageFactory();
      }

      private static TcpSocket InterfaceTcp()
      {
        return new TcpSocket();
      }

      private MessageOutbound OutputConfiguration(TcpSocket tcpSocket, XmlOutboundMessageFactory xmlOutboundMessageFactory)
      {
        return new MessageOutbound(tcpSocket, xmlOutboundMessageFactory);
      }

      private static BinaryParsing FormatBinary()
      {
        return new BinaryParsing();
      }

      private static UdpSocket InterfaceUdp()
      {
        return new UdpSocket();
      }

      private MessageInbound InputConfiguration(UdpSocket udpSocket, BinaryParsing binaryParsing)
      {
        return new MessageInbound(udpSocket, binaryParsing);
      }

      private TeleComSystem TeleComSystem(MessageInbound messageInbound, AcmeProcessingWorkflow acmeProcessingWorkflow, MessageOutbound messageOutbound)
      {
        return new TeleComSystem(messageInbound, messageOutbound, acmeProcessingWorkflow);
      }


    }
}
