using DependencyInjectionBefore._1_ControlFreak.Inbound;
using DependencyInjectionBefore._1_ControlFreak.Outbound;

namespace DependencyInjectionBefore._1_ControlFreak.Core
{
  class TeleComSystem
  {
    private readonly AcmeProcessingWorkflow _processingWorkflow;
    private readonly BinaryUdpInbound _inbound;
    private readonly XmlTcpOutbound _outbound;

    public TeleComSystem()
    {
      _inbound = new BinaryUdpInbound();
      _outbound = new XmlTcpOutbound();
      _processingWorkflow = new AcmeProcessingWorkflow();
    }

    public void Start()
    {
      _inbound.SetDomainLogic(_processingWorkflow);
      _processingWorkflow.SetOutbound(_outbound);
      _inbound.StartListening();
    }
  }
}