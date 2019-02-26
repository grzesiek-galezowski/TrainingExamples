using Functional.Maybe;

namespace PloehKata
{
    public interface IPersistence
    {
        Maybe<T> ReadById<T>(string resourceName, string id);
    }

    public class NoSqlPersistence : IPersistence
    {
        public Maybe<T> ReadById<T>(string resourceName, string id)
        {
            throw new System.NotImplementedException();
        }
    }
}