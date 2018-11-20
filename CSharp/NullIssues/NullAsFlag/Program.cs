using System;

namespace NullAsFlag
{
    class Program
    {
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
                return null;
            }
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
