using System;

namespace NullAsFlag;

// This is one of the programs invoked in a chain with a command
// not every program supports all the commands
class _01_ReturningNull
{
    //null as do-nothing - returned value
    static void Main(string[] args)
    {
        var commandFactory = new CommandFactory();
        var command = commandFactory.CreateCommandFrom(args);

        //this code will appear everywhere we need a factory
        if (command != null)
        {
            command.Execute();
        }
    }
}

internal class CommandFactory
{
    public IConfigurationCommand CreateCommandFrom(string[] args)
    {
        if (IsChangeIdCommand(args))
        {
            return new ChangeIdCommand();
        }
        else if (IsSynchronizeCommand(args))
        {
            return new SynchronizeAllCommand();
        }
        else
        {
            //Assume this isn't an error. This cmdline
            //tool just doesn't support all the commands
            return null;
        }
    }



    private static bool IsSynchronizeCommand(string[] args)
    {
        return args.Length == 2;
    }

    private static bool IsChangeIdCommand(string[] args)
    {
        return args.Length == 1;
    }
}

internal class SynchronizeAllCommand : IConfigurationCommand
{
    public void Execute()
    {
        throw new NotImplementedException();
    }
}

internal class ChangeIdCommand : IConfigurationCommand
{
    public void Execute()
    {
        throw new NotImplementedException();
    }
}

internal interface IConfigurationCommand
{
    void Execute();
}