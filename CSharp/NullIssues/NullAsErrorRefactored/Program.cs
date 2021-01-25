using System;

namespace NullAsErrorRefactored
{
    //exceptions have 2 things nulls do not:
    //1. A name
    //2. A stack trace

    class Program
    {
        public static void Main(string[] args)
        {
            var database = new Database();
            var userDto = new UserDto();

            try
            {
                database.Save(userDto);
            }
            catch (DatabaseConnectionException e)
            {
                LogError(e, $"Could not connect to db to save user {userDto}");
            }
        }





        private static void LogError(DatabaseConnectionException databaseConnectionException, string s)
        {
            Console.WriteLine(s);
        }
    }

    internal class DatabaseConnectionException : Exception
    {
    }

    internal class ResourceId
    {
    }

    public class UserDto
    {
    }

    internal class Database
    {
        public void Save(UserDto userDto)
        {
        }
    }
}
