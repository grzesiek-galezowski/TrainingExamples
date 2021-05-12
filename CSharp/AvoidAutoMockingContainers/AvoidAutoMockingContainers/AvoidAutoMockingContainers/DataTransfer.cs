namespace AvoidAutoMockingContainers
{
    public class DataTransfer // :-)
    {
        private readonly IDestination _destination;
        private readonly ISource _source;

        public DataTransfer(IDestination destination, ISource source)
        {
            _destination = destination;
            _source = source;
        }

        public void Commence()
        {
            _destination.Save(_source.Read());
        }
    }
}