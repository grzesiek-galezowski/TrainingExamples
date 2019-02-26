namespace PloehKata
{
    public class UserDestination : IConnectorDestination
    {
        public UserDestination(NoSqlPersistence noSqlPersistence)
        {
          
        }

        public void Save(UserDto userDto)
        {
            throw new System.NotImplementedException();
        }
    }
}