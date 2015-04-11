using TelecomSystemNestedFunctions.Core;
using TelecomSystemNestedFunctions.Inbound;
using TelecomSystemNestedFunctions.Outbound;
using TelecomSystemNestedFunctions.Services;

namespace TelecomSystemNestedFunctions
{
  public class ApplicationRoot2
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
        Messages(
          ReceivedThroughUdp().In(FormatBinary()), 
          Authenticated().Then(Persisted()), 
          SentThroughTcp().In(FormatXml()));

      sys.Start();
    }


    public static MsSqlBasedRepository Persisted()
    {
      return new MsSqlBasedRepository(
        new SqlDataDestination());
    }

    public static ActiveDirectoryBasedAuthorization Authenticated()
    {
      return new ActiveDirectoryBasedAuthorization();
    }

    public static XmlOutboundMessageFactory FormatXml()
    {
      return new XmlOutboundMessageFactory();
    }

    public static TcpSocket SentThroughTcp()
    {
      return new TcpSocket();
    }

    public static BinaryParsing FormatBinary()
    {
      return new BinaryParsing();
    }

    public static UdpSocket ReceivedThroughUdp()
    {
      return new UdpSocket();
    }

    public static TeleComSystem Messages(MessageInbound messageInbound, AcmeProcessingWorkflow acmeProcessingWorkflow, MessageOutbound messageOutbound)
    {
      return new TeleComSystem(messageInbound, messageOutbound, acmeProcessingWorkflow);
    }

  }

  public static class TeleComExtensions
  {


    public static AcmeProcessingWorkflow Then(this ActiveDirectoryBasedAuthorization activeDirectoryBasedAuthorization, MsSqlBasedRepository msSqlBasedRepository)
    {
      return new AcmeProcessingWorkflow(activeDirectoryBasedAuthorization, msSqlBasedRepository);
    }

    public static MessageOutbound In(this TcpSocket tcpSocket, XmlOutboundMessageFactory xmlOutboundMessageFactory)
    {
      return new MessageOutbound(tcpSocket, xmlOutboundMessageFactory);
    }

    public static MessageInbound In(this UdpSocket udpSocket, BinaryParsing binaryParsing)
    {
      return new MessageInbound(udpSocket, binaryParsing);
    }





  }
}