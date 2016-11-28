using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Core;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Inbound
{
  internal interface IInbound
  {
    void SetDomainLogic(IAcmeProcessingWorkflow processingWorkflow);
    void StartListening();
  }

  class MessageInbound : IInbound
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