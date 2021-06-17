using ConformingContainerAntipattern._3_ConformingContainer.Core;

namespace ConformingContainerAntipattern._3_ConformingContainer.Inbound
{
  internal interface IInbound
  {
    void SetDomainLogic(IProcessingWorkflow processingWorkflow);
    void StartListening();
  }

  class BinaryUdpInbound : IInbound
  {
    private IProcessingWorkflow _processingWorkflow;
    private readonly IInputSocket _socket;
    private readonly IPacketParsing _parsing;

    public BinaryUdpInbound()
    {
      _socket = ApplicationRoot.Context.Resolve<IInputSocket>();
      _parsing = ApplicationRoot.Context.Resolve<IPacketParsing>();
    }

    public void SetDomainLogic(IProcessingWorkflow processingWorkflow)
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