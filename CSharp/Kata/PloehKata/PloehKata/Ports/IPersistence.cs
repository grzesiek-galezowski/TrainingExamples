using Functional.Maybe;

namespace PloehKata.Ports
{
    public interface IPersistence
    {
      Maybe<T> ReadById<T>(string resourceName, string id);
      void Save(string resourceName, UserDto userDto);
    }

    public class NoSqlPersistence : IPersistence
    {
      public Maybe<T> ReadById<T>(string resourceName, string id)
        {
            throw new System.NotImplementedException();
        }

      public void Save(string resourceName, UserDto userDto)
        {
          throw new System.NotImplementedException();
        }
    }
}