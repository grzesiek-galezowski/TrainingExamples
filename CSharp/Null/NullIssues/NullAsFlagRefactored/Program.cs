using System;

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
            if (IsChangeIdCommand(args))
            {
                return new ChangeIdCommand();
            }
            else if (IsSynchronizeAllCommand(args))
            {
                return new SynchronizeAllCommand();
            }
            else
            {
                return new IgnoredCommand();
            }
        }

        private static bool IsSynchronizeAllCommand(string[] args)
        {
            return args.Length == 2;
        }

        private static bool IsChangeIdCommand(string[] args)
        {
            return args.Length == 1;
        }
    }

    //null object pattern implements a __NEUTRAL_OPERATION__
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
