package ServiceLocatorAntipattern;

import ServiceLocatorAntipattern.Core.AcmeProcessingWorkflow;
import ServiceLocatorAntipattern.Core.ProcessingWorkflow;
import ServiceLocatorAntipattern.Core.TeleComSystem;
import ServiceLocatorAntipattern.InMessages.NullMessage;
import ServiceLocatorAntipattern.InMessages.StartMessage;
import ServiceLocatorAntipattern.InMessages.StopMessage;
import ServiceLocatorAntipattern.Inbound.BinaryParsing;
import ServiceLocatorAntipattern.Inbound.BinaryUdpInbound;
import ServiceLocatorAntipattern.Inbound.IInbound;
import ServiceLocatorAntipattern.Inbound.PacketParsing;
import ServiceLocatorAntipattern.Outbound.*;
import ServiceLocatorAntipattern.Services.*;
import org.picocontainer.ComponentMonitor;
import org.picocontainer.DefaultPicoContainer;
import org.picocontainer.MutablePicoContainer;
import org.picocontainer.lifecycle.ReflectionLifecycleStrategy;
import org.picocontainer.monitors.NullComponentMonitor;

import java.util.Random;

import static org.picocontainer.Characteristics.CACHE;
import static org.picocontainer.Characteristics.NO_CACHE;


public class ApplicationRoot {
  public static final MutablePicoContainer context = createContainer();

  private static MutablePicoContainer createContainer() {
    ComponentMonitor monitor = new NullComponentMonitor();
    MutablePicoContainer context = new DefaultPicoContainer(
        monitor,
        new ReflectionLifecycleStrategy(monitor),
        null);
    context.as(CACHE)
        .addComponent(IRepository.class, MsSqlBasedRepository.class)
        .addComponent(IAuthorization.class, ActiveDirectoryBasedAuthorization.class)
        .addComponent(ProcessingWorkflow.class, AcmeProcessingWorkflow.class)
        .addComponent(OutputSocket.class, TcpSocket.class)
        .addComponent(Outbound.class, MessageOutbound.class)
        .addComponent(PacketParsing.class, BinaryParsing.class)
        .addComponent(IInbound.class, BinaryUdpInbound.class)
        // forgot about this... context.addComponent(IMarshalling.class, XmlMarshalling.class);
        .addComponent(SqlDataDestination.class);
    context.as(NO_CACHE)
        .addComponent(IOutboundMessage.class, OutboundMessage.class)
        .addComponent(Random.class)
        .addComponent(NullMessage.class)
        .addComponent(StartMessage.class)
        .addComponent(StopMessage.class);

    return context;
  }

  public static void Main(String[] args) {
    try {
      TeleComSystem sys = new TeleComSystem(); //uses container inside, but should be resolved itself!
      sys.start();

    } finally {
      context.dispose();
    }
  }
}