namespace AvoidAutoMockingContainers
{
    public class TransferUtilHelperManager // :-)
    {
        private readonly IDestination _destination;
        private readonly ISource _source;

        public TransferUtilHelperManager(IDestination destination, ISource source)
        {
            _destination = destination;
            _source = source;
        }

        public void TransferData()
        {
            _destination.Save(_source.Read());
        }
    }
}