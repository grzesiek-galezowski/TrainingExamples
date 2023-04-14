using System;

namespace NullAsError;

class Program
{
    static void Main(string[] args)
    {
        var database = new Database();
        var userDto = new UserDto();

        ResourceId id = database.Save(userDto);

        if (id == null)
        {
            LogError($"Could not connect to db to save user {userDto}");
        }
    }

    private static void LogError(string s)
    {
        Console.WriteLine(s);
    }
}

internal class Database
{
    public ResourceId Save(UserDto userDto)
    {
        return new ResourceId();
    }
}

internal record ResourceId
{
}

public record UserDto
{
}