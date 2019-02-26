using System;

namespace PloehKata
{
    public class NoConnectee : IConnectee
    {
        public void AttemptConnectionFrom(IExistingConnector connector, IConnectionInProgress connectionInProgress)
        {
            throw new NotImplementedException();
        }
    }
}