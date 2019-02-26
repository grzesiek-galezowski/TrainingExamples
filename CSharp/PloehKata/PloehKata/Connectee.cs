namespace PloehKata
{
    public class Connectee : IConnectee
    {
        private readonly string _id;

        public Connectee(string id)
        {
            _id = id;
        }

        public void AttemptConnectionFrom(IExistingConnector connector, IConnectionInProgress connectionInProgress)
        {
            connector.ConnectWith(_id);
        }
    }
}