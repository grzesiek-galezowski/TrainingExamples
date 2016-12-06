import java.util.Random;

import static com.sun.deploy.trace.TraceLevel.CACHE;

public class ApplicationRoot {
      static {
        ComponentMonitor monitor = new NullComponentMonitor();
        context = new DefaultPicoContainer(
          monitor,
          new ReflectionLifecycleStrategy(monitor),
          null);
        context.as(CACHE)
            .addComponent(IRepository.class, MsSqlBasedRepository.class)
            .addComponent(IAuthorization.class, ActiveDirectoryBasedAuthorization.class)
            .addComponent(IAcmeProcessingWorkflow.class, AcmeProcessingWorkflow.class)
            .addComponent(IOutboundMessageFactory.class, XmlOutboundMessageFactory.class)
            .addComponent(ISocket.class, TcpSocket.class)
            .addComponent(IOutbound.class, MessageOutbound.class)
            .addComponent(IParsing.class, BinaryParsing.class)
            .addComponent(IInbound.class, MessageInbound.class);
        // forgot about this... context.addComponent(IMarshalling.class, XmlMarshalling.class);
            .addComponent(SqlDataDestination.class);
// forgot about this... Context.RegisterType<IMarshalling, XmlMarshalling>(new ContainerControlledLifetimeManager());


        
        //not container controlled
        Context.RegisterType<IOutboundMessage, OutboundMessage>();
        Context.RegisterType<Random>(new InjectionFactory(container => new Random()));
        Context.RegisterType<NullMessage>();
        Context.RegisterType<StartMessage>();
        Context.RegisterType<StopMessage>();
      }

      public static void Main(string[] args)
      {
        try
        {
          var sys = new TeleComSystem(); //uses container inside, but should be resolved itself!
          sys.Start();

        }
        finally
        {
          context.Dispose();
        }
      }

      //simplified

      public static final MutablePicoContainer context;
    }

    /*
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

     */