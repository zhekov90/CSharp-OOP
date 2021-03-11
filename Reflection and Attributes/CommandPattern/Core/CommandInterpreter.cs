
using CommandPattern.Core.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string commandPostfix = "Command";
        public CommandInterpreter()
        {

        }
        public string Read(string args)
        {
            string[] commandTokens = args.Split(' ', System.StringSplitOptions.RemoveEmptyEntries).ToArray();

            string commandName = commandTokens[0] + commandPostfix;
            string[] commandArgs = commandTokens.Skip(1).ToArray();

            Assembly assembly = Assembly.GetCallingAssembly();

            Type commandType = assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name.ToLower() ==commandName.ToLower());

            if (commandType==null)
            {
                throw new ArgumentException("Invalid command type!");
            }

            ICommand commandInstance = (ICommand)Activator.CreateInstance(commandType);

            string result = commandInstance.Execute(commandArgs);
            return result;
        }
    }
}
