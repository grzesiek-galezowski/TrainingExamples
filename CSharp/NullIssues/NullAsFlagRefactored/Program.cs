using System;
using System.Runtime.InteropServices;

namespace NullAsFlagRefactored
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandFactory = new CommandFactory();
            var command = commandFactory.CreateCommandFrom(args);

            command.Execute();
        }
    }

    internal class CommandFactory
    {
        public IConfigurationCommand CreateCommandFrom(string[] args)
        {
            if (args.Length == 1)
            {
                return new ChangeIdCommand();
            }
            else if (args.Length == 2)
            {
                return new SynchronizeAllCommand();
            }
            else
            {
                return new IgnoredCommand();
            }
        }
    }

    public class IgnoredCommand : IConfigurationCommand
    {
        public void Execute()
        {
            //do nothing or log something
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
}
