import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.AcmeProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.IAcmeProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.TeleComSystem;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound.*;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.*;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.*;
import org.picocontainer.ComponentMonitor;
import org.picocontainer.DefaultPicoContainer;
import org.picocontainer.MutablePicoContainer;
import org.picocontainer.lifecycle.ReflectionLifecycleStrategy;
import org.picocontainer.monitors.NullComponentMonitor;

import static org.picocontainer.Characteristics.CACHE;

public class ApplicationRoot {

  public void mainBareConstructors() {
    TeleComSystem sys = new TeleComSystem(
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

  public void mainFluentInterfaceWay() {
    //Growing Object Oriented Software Guided By Tests
    //Building on SOLID Foundations
    TeleComSystem sys = aSystemThat(
        receivesMessages(
            viaUdp(),
            inBinaryFormat()),
        andThen(
            authenticatesThemViaActiveDirectory(),
            persistsInSqlDatabase()),
        andSendsThem(viaTcp(), asXml()));

    sys.Start();
  }

  public void mainIoCContainer() {
    MutablePicoContainer pico = null;

    try
    {
      ComponentMonitor monitor = new NullComponentMonitor();
      pico = new DefaultPicoContainer(
          monitor,
          new ReflectionLifecycleStrategy(monitor),
          null).as(CACHE);

      //Register
      pico
          .addComponent(IRepository.class, MsSqlBasedRepository.class)
          .addComponent(IAuthorization.class, ActiveDirectoryBasedAuthorization.class)
          .addComponent(IAcmeProcessingWorkflow.class, AcmeProcessingWorkflow.class)
          .addComponent(IOutboundMessageFactory.class, XmlOutboundMessageFactory.class)
          .addComponent(ISocket.class, TcpSocket.class)
          .addComponent(IOutbound.class, MessageOutbound.class)
          .addComponent(IParsing.class, BinaryParsing.class)
          .addComponent(IInbound.class, MessageInbound.class)
          .addComponent(TeleComSystem.class);
      //////// Only one getComponent() call after this line! ////////////

      //Resolve
      TeleComSystem system = pico.getComponent(TeleComSystem.class);
      system.Start();
    }
    finally
    {
      //Release
      if(pico != null) pico.dispose();
    }

  }


  private static MsSqlBasedRepository persistsInSqlDatabase() {
    return new MsSqlBasedRepository(
        new SqlDataDestination());
  }

  private static ActiveDirectoryBasedAuthorization authenticatesThemViaActiveDirectory() {
    return new ActiveDirectoryBasedAuthorization();
  }

  private AcmeProcessingWorkflow andThen(ActiveDirectoryBasedAuthorization activeDirectoryBasedAuthorization, MsSqlBasedRepository msSqlBasedRepository) {
    return new AcmeProcessingWorkflow(activeDirectoryBasedAuthorization, msSqlBasedRepository);
  }

  private static XmlOutboundMessageFactory asXml() {
    return new XmlOutboundMessageFactory();
  }

  private static TcpSocket viaTcp() {
    return new TcpSocket();
  }

  private MessageOutbound andSendsThem(TcpSocket tcpSocket, XmlOutboundMessageFactory xmlOutboundMessageFactory) {
    return new MessageOutbound(tcpSocket, xmlOutboundMessageFactory);
  }

  private static BinaryParsing inBinaryFormat() {
    return new BinaryParsing();
  }

  private static UdpSocket viaUdp() {
    return new UdpSocket();
  }

  private MessageInbound receivesMessages(UdpSocket udpSocket, BinaryParsing binaryParsing) {
    return new MessageInbound(udpSocket, binaryParsing);
  }

  private TeleComSystem aSystemThat(MessageInbound messageInbound, AcmeProcessingWorkflow acmeProcessingWorkflow, MessageOutbound messageOutbound) {
    return new TeleComSystem(messageInbound, messageOutbound, acmeProcessingWorkflow);
  }


}


