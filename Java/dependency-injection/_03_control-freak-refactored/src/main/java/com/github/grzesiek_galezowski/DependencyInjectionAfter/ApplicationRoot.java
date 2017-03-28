package com.github.grzesiek_galezowski.DependencyInjectionAfter;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.AcmeProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.ProcessingWorkflow;
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

    sys.start();
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

    sys.start();
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
          .addComponent(Repository.class, MsSqlBasedRepository.class)
          .addComponent(Authorization.class, ActiveDirectoryBasedAuthorization.class)
          .addComponent(ProcessingWorkflow.class, AcmeProcessingWorkflow.class)
          .addComponent(OutboundMessageFactory.class, XmlOutboundMessageFactory.class)
          .addComponent(Socket.class, TcpSocket.class)
          .addComponent(Outbound.class, MessageOutbound.class)
          .addComponent(Parsing.class, BinaryParsing.class)
          .addComponent(Inbound.class, MessageInbound.class)
          .addComponent(TeleComSystem.class);
      //////// Only one getComponent() call after this line! ////////////

      //Resolve
      TeleComSystem system = pico.getComponent(TeleComSystem.class);
      system.start();
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


