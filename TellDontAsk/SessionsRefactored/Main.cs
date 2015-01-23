using System;
using SessionsRefactored.Destinations;
using SessionsRefactored.GUI;
using SessionsRefactored.Network;
using SessionsRefactored.SessionsTypes;

namespace SessionsRefactored
{
  public class Main
  {
    public void Program()
    {
      ///////////////////////////////////////
      // 1. Hiding access to sessions allows protecting the collection against
      //    concurrent additions
      Sessions sessions = new SynchronizedSessions(new BasicSessions());
      AddExemplaryDataTo(sessions);

      // different destinations where sessions can be dumped:
      var consoleDestination = new ConsoleDestination();
      var networkPacketBuilder = new NetworkPacketBuilder();
      var fileDestination1 = new FileStorageFormat1();
      var fileDestination2 = new FileStorageFormat2();
      var devNull = new DevNull();
      var populationOfOwnersListOnGui = new PopulationOfOwnersListOnGui(new WpfBasedOwnersList());

      ///////////////////////////////////////
      // 2. These two prove that we have eliminated redundancy in:
      //    a) what data belongs to session
      //    b) in which order they are dumped
      sessions.DumpTo(consoleDestination);
      sessions.DumpTo(fileDestination1);

      ///////////////////////////////////////
      // 3. These three prove that we have not lost flexibility we had with the getters approach:

      //    additionally these two always want to write _all_ of the session fields, so they belong to 2.a. as well
      sessions.DumpTo(fileDestination2); 
      sessions.DumpTo(networkPacketBuilder);
      var networkConnection = new BogusNetworkConnection();
      networkPacketBuilder.SendBuiltPacketsThrough(networkConnection);

      sessions.DumpTo(populationOfOwnersListOnGui);
      sessions.DumpTo(devNull);
    }

    private static void AddExemplaryDataTo(Sessions sessions)
    {
      //basic session
      sessions.Add(new BasicSession(new SessionData()
      {
        Id = 1,
        Owner = "Zenek",
        Target = "Astro device"
      }));

      // proves that sessions can take advantage of the control over dumping session data:
      // by dumping only if session is not expired
      sessions.Add(new ExpirableSession(new BasicSession(new SessionData()
      {
        Id = 2,
        Owner = "Janek",
        Target = "Dimetra device"
      }), DateTime.Now.AddDays(3)));

      // proves that sessions can take advantage of the control over dumping session data
      // by not dumping at all
      sessions.Add(new HiddenSession(new BasicSession(new SessionData()
      {
        Id = 3,
        Owner = "Czesiek",
        Target = "LTE device"
      })));
    }
  }
}