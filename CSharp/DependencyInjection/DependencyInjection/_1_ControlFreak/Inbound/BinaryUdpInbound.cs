using DependencyInjectionBefore._1_ControlFreak.Core;

namespace DependencyInjectionBefore._1_ControlFreak.Inbound
{
  class BinaryUdpInbound
  {
    private AcmeProcessingWorkflow _processingWorkflow;
    private readonly UdpSocket _socket;
    private readonly BinaryParsing _parsing;

    public BinaryUdpInbound()
    {
      _socket = new UdpSocket();
      _parsing = new BinaryParsing();
    }

    public void SetDomainLogic(AcmeProcessingWorkflow processingWorkflow)
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