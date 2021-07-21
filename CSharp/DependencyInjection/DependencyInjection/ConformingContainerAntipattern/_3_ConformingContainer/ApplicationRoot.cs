using System;
using ConformingContainerAntipattern._3_ConformingContainer.Core;
using ConformingContainerAntipattern._3_ConformingContainer.InMessages;
using ConformingContainerAntipattern._3_ConformingContainer.Inbound;
using ConformingContainerAntipattern._3_ConformingContainer.Outbound;
using ConformingContainerAntipattern._3_ConformingContainer.Services;

namespace ConformingContainerAntipattern._3_ConformingContainer
{
    public static class ApplicationRoot
    {
      static ApplicationRoot()
      {
        Context.For<IProcessingWorkflow>().UseAlwaysTheSame<AcmeProcessingWorkflow>();
        Context.For<IInbound>().UseAlwaysTheSame<BinaryUdpInbound>();
        Context.For<IInputSocket>().UseAlwaysTheSame<UdpSocket>();
        Context.For<IOutputSocket>().UseAlwaysTheSame<TcpSocket>();
        Context.For<IPacketParsing>().UseAlwaysTheSame<BinaryParsing>();
        Context.For<IOutbound>().UseAlwaysTheSame<Outbound.Outbound>();
        Context.For<IAuthorization>().UseAlwaysTheSame<ActiveDirectoryBasedAuthorization>();
        Context.For<IRepository>().UseAlwaysTheSame<MsSqlBasedRepository>();
        Context.For<IMarshalling>().UseAlwaysTheSame<XmlMarshalling>();

        Context.UseAlwaysTheSame<SqlDataDestination>();

        //not container controlled
        Context.For<IOutboundMessage>().UseEachTimeNew<OutboundMessage>();
        Context.For<Random>().UseInstancesCreatedWith(container => new Random());
        Context.UseEachTimeNew<NullMessage>();
        Context.UseEachTimeNew<StartMessage>();
        Context.UseEachTimeNew<StopMessage>();
      }

      public static void Main2(string[] args)
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
      public static readonly ConformingContainer Context = new ConformingContainer();
    }


}
