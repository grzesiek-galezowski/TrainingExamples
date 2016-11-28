import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.AcmeProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.IAcmeProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.TeleComSystem;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound.*;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.*;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.*;
import org.picocontainer.DefaultPicoContainer;
import org.picocontainer.MutablePicoContainer;
import org.picocontainer.lifecycle.ReflectionLifecycleStrategy;
import org.picocontainer.monitors.NullComponentMonitor;

import static org.picocontainer.Characteristics.CACHE;

public class ApplicationRoot {

  public void Main_BareConstructors() {
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

  public void Main_FluentInterfaceWay() {
    //Growing Object Oriented Software Guided By Tests
    //Building on SOLID Foundations
    TeleComSystem sys = ASystemThat(
        ReceivesMessages(
            ViaUdp(),
            InBinaryFormat()),
        AndThen(
            AuthenticatesThemViaActiveDirectory(),
            PersistsInSqlDatabase()),
        AndSendsThem(ViaTcp(), AsXml()));

    sys.Start();
  }

  public void Main_IoC_Container() {
    MutablePicoContainer pico = null;

    try
    {
      pico = new DefaultPicoContainer().as(CACHE); //todo add ReflectionLifecycleStrategy

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
      //////// Only one Resolve() call after this line! ////////////

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


  private static MsSqlBasedRepository PersistsInSqlDatabase() {
    return new MsSqlBasedRepository(
        new SqlDataDestination());
  }

  private static ActiveDirectoryBasedAuthorization AuthenticatesThemViaActiveDirectory() {
    return new ActiveDirectoryBasedAuthorization();
  }

  private AcmeProcessingWorkflow AndThen(ActiveDirectoryBasedAuthorization activeDirectoryBasedAuthorization, MsSqlBasedRepository msSqlBasedRepository) {
    return new AcmeProcessingWorkflow(activeDirectoryBasedAuthorization, msSqlBasedRepository);
  }

  private static XmlOutboundMessageFactory AsXml() {
    return new XmlOutboundMessageFactory();
  }

  private static TcpSocket ViaTcp() {
    return new TcpSocket();
  }

  private MessageOutbound AndSendsThem(TcpSocket tcpSocket, XmlOutboundMessageFactory xmlOutboundMessageFactory) {
    return new MessageOutbound(tcpSocket, xmlOutboundMessageFactory);
  }

  private static BinaryParsing InBinaryFormat() {
    return new BinaryParsing();
  }

  private static UdpSocket ViaUdp() {
    return new UdpSocket();
  }

  private MessageInbound ReceivesMessages(UdpSocket udpSocket, BinaryParsing binaryParsing) {
    return new MessageInbound(udpSocket, binaryParsing);
  }

  private TeleComSystem ASystemThat(MessageInbound messageInbound, AcmeProcessingWorkflow acmeProcessingWorkflow, MessageOutbound messageOutbound) {
    return new TeleComSystem(messageInbound, messageOutbound, acmeProcessingWorkflow);
  }


}


