using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNoMockSpecification._08_PeersInternals;

public class BufferingObserverSpecification
{
  [Test]
  public void ShouldSendAllBufferedEventsToNextObserver()
  {
    //GIVEN
    var observer = Substitute.For<IMyObserver>();
    var bufferedObserver = new BufferedObserver(observer);
    var event1 = Any.Instance<IMyEvent>();
    var event2 = Any.Instance<IMyEvent>();
    var event3 = Any.Instance<IMyEvent>();
    var event4 = Any.Instance<IMyEvent>();

    bufferedObserver.Send(event1, event2);
    bufferedObserver.Send(event3, event4);

    //WHEN
    bufferedObserver.Flush();

    //THEN
    observer.Received(1).Send(event1, event2, event3, event4);
  }

  [Test]
  public void ShouldNotSendTheSameEventsTwice()
  {
    //GIVEN
    var observer = Substitute.For<IMyObserver>();
    var bufferedObserver = new BufferedObserver(observer);
    var event1 = Any.Instance<IMyEvent>();
    var event2 = Any.Instance<IMyEvent>();
    var event3 = Any.Instance<IMyEvent>();
    var event4 = Any.Instance<IMyEvent>();

    bufferedObserver.Send(event1, event2);
    bufferedObserver.Send(event3, event4);

    //WHEN
    bufferedObserver.Flush();
    observer.ClearReceivedCalls();
    bufferedObserver.Flush();

    //THEN
    observer.Received(1).Send();
  }
}