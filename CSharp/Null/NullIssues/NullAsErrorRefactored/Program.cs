using System;

namespace NullAsErrorRefactored;
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
        //btw, often, this is a better way:
        //catch (Exception e)
        //{
        //    //the details are in the exception
        //    LogError(e, $"Request failed");
        //}

        //btw, "throw exceptions in exceptional situations" - what about it?
        //btw, what about monadic return types like Either? Or error callbacks?
    }





    private static void LogError(DatabaseConnectionException databaseConnectionException, string s)
    {
        Console.WriteLine(s);
    }
}

internal class DatabaseConnectionException : Exception
{
}

internal record ResourceId
{
}

public record UserDto
{
}

internal class Database
{
    public void Save(UserDto userDto)
    {
    }
}