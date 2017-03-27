package com.github.grzesiek_galezowski.ConformingContainerAntipattern;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core.AcmeProcessingWorkflow;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core.ProcessingWorkflow;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core.TeleComSystem;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.InMessages.NullMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.InMessages.StartMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.InMessages.StopMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound.*;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound.*;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services.*;

import java.util.Random;

public class ApplicationRoot {
  public static final ConformingContainer CONTEXT = createContainer();

  private static ConformingContainer createContainer() {
    CONTEXT.as(ProcessingWorkflow.class).useSingle(AcmeProcessingWorkflow.class);
    CONTEXT.as(Inbound.class).useSingle(BinaryUdpInbound.class);
    CONTEXT.as(InputSocket.class).useSingle(UdpSocket.class);
    CONTEXT.as(OutputSocket.class).useSingle(TcpSocket.class);
    CONTEXT.as(PacketParsing.class).useSingle(BinaryParsing.class);
    CONTEXT.as(Outbound.class).useSingle(MessageOutbound.class);
    CONTEXT.as(Authorization.class).useSingle(ActiveDirectoryBasedAuthorization.class);
    CONTEXT.as(Repository.class).useSingle(MsSqlBasedRepository.class);
    CONTEXT.as(Marshalling.class).useSingle(XmlMarshalling.class);

    CONTEXT.useSingle(SqlDataDestination.class);

    //not container controlled
    CONTEXT.as(OutboundMessage.class).useCreated(XmlOutboundMessage.class);
    CONTEXT.as(Random.class).useCreated(Random.class);
    CONTEXT.useCreated(NullMessage.class);
    CONTEXT.useCreated(StartMessage.class);
    CONTEXT.useCreated(StopMessage.class);
    return CONTEXT;
  }

  public static void Main(String[] args) throws Exception {
    try {
      TeleComSystem sys = new TeleComSystem(); //uses container inside, but should be resolved itself!
      sys.start();
    } finally {
      CONTEXT.close();
    }
  }
}


