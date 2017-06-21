using TelecomSystemNestedFunctions.Core;

namespace TelecomSystemNestedFunctions.Inbound
{
  public interface IInbound
  {
    void SetDomainLogic(IAcmeProcessingWorkflow processingWorkflow);
    void StartListening();
  }

  public class MessageInbound : IInbound
  {
    private IAcmeProcessingWorkflow _processingWorkflow;
    private readonly IInboundSocket _socket;
    private readonly IParsing _parsing;

    public MessageInbound(
      IInboundSocket udpSocket, IParsing binaryParsing)
    {
      _socket = udpSocket;
      _parsing = binaryParsing;
    }

    public void SetDomainLogic(IAcmeProcessingWorkflow processingWorkflow)
    {
      _processingWorkflow = processingWorkflow;
    }
    
    public void StartListening()
    {
      byte[] frameData;
      while (_socket.Receive(out frameData))
      {
        var message = _parsing.ResultFor(frameData);
        if (message != null)
        {
          if (_processingWorkflow != null)
          {
            _processingWorkflow.ApplyTo(message);
          }
        }
      }
    }
  }
}