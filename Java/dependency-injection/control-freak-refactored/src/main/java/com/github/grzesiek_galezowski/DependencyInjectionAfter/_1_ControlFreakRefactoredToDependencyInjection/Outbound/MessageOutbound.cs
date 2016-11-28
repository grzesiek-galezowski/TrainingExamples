using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Outbound
{
  public interface IOutbound
  {
    void Send(AcmeMessage message);
  }

  public class MessageOutbound : IOutbound
  {
    private readonly ISocket _outputSocket;
    private readonly IOutboundMessageFactory _outboundMessageFactory;

    public MessageOutbound(
      ISocket outputSocket, 
      IOutboundMessageFactory outboundMessageFactory)
    {
      _outputSocket = outputSocket;
      _outboundMessageFactory = outboundMessageFactory;
    }

    public void Send(AcmeMessage message)
    {
      var outboundMessage = _outboundMessageFactory.CreateOutboundMessage();
      message.WriteTo(outboundMessage);
      outboundMessage.SendVia(_outputSocket);
    }
  }
}