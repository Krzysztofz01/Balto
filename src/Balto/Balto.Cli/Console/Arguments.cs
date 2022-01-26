using Balto.Cli.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Balto.Cli.Console
{
    public class Arguments : IArguments
    {
        private readonly bool _helpInvoked;
        private readonly int _count;
        private readonly string _moduleSelector;

        private readonly Dictionary<string, string> _properties;
        private readonly HashSet<string> _commands;
        private readonly HashSet<string> _flags;

        private Arguments() { }
        public Arguments(string[] args)
        {
            _properties = new Dictionary<string, string>();
            _commands = new HashSet<string>();
            _flags = new HashSet<string>();

            ParseArguments(args);

            _count = args.Length;
            _helpInvoked = IsFlagSet("--help");
            _moduleSelector = _commands.First();
        }

        private void ParseArguments(string[] args)
        {
            string previousFlagOrProp = null;

            foreach (var arg in args)
            {
                if (arg.StartsWith("--"))
                {
                    if (previousFlagOrProp is not null)
                    {
                        _flags.Add(previousFlagOrProp.ToLower());
                    }

                    previousFlagOrProp = arg;
                }
                else
                {
                    if (previousFlagOrProp is not null)
                    {
                        _properties.Add(previousFlagOrProp.ToLower(), arg);

                        previousFlagOrProp = null;
                    }
                    else
                    {
                        _commands.Add(arg.ToLower());
                    }
                }
            }

            if (previousFlagOrProp is not null)
            {
                _flags.Add(previousFlagOrProp.ToLower());
            }
        }

        public bool IsHelp => _helpInvoked;
        public int Count => _count;
        public bool Any => _count > 0;
        public string GetModuleSelector => _moduleSelector;
        public int FlagCount => _flags.Count;
        public int CommandCount => _commands.Count;
        public int PropertyCount => _properties.Count;

        public bool IsFlagSet(string flagName)
        {
            return _flags.Contains(flagName.ToLower());
        }

        public bool IsCommandUsed(string commandName)
        {
            return _commands.Contains(commandName.ToLower());
        }

        public string GetPropertyValue(string propertyName)
        {
            return _properties[propertyName.ToLower()];
        }

        public string GetPropertyValueOrDefault(string propertyName)
        {
            return _properties.GetValueOrDefault(propertyName.ToLower());
        }
    }
}
