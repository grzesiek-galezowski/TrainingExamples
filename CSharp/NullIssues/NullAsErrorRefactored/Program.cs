using System;

namespace NullAsErrorRefactored
{
    class Program
    {
        static void Main(string[] args)
        {
            var database = new Database();
            var userDto = new UserDto();

            try
            {
                var id = database.Save(userDto);
            }
            catch (DatabaseConnectionException e)
            {

            }

            if (id == null)
            {
                LogError(
                    $"Could not connect to db to save user {userDto}");
            }
        }

        private static void LogError(string s)
        {
            Console.WriteLine(s);
        }
    }

    internal class ResourceId
    {
    }

    public class UserDto
    {
    }

    internal class Database
    {
        public ResourceId Save(UserDto userDto)
        {
        }
    }
}
