using Balto.Cli.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balto.Cli.Console
{
    public class Arguments : IArguments
    {
        private readonly bool _helpInvoked;
        private readonly int _count;
        private readonly string _moduleSelector;
        private readonly string _submoduleSelector;

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
            _moduleSelector = _commands.FirstOrDefault();
            _submoduleSelector = GetSubmoduleOrDefault();

            if (_moduleSelector is null)
            {
                _helpInvoked = true;
                _moduleSelector = "help";
            }
        }

        private void ParseArguments(string[] args)
        {
            try
            {
                string previousFlagOrProp = null;

                StringBuilder spacedProp = null;

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
                        if (arg.StartsWith('"') && spacedProp is null)
                        {
                            spacedProp = new StringBuilder(arg.Replace('"', default));
                            spacedProp.Append(' ');
                            continue;
                        }

                        if (previousFlagOrProp is not null)
                        {
                            if (spacedProp is not null)
                            {
                                string clearedArg = arg.Replace('"', default);

                                spacedProp.Append(clearedArg);
                                spacedProp.Append(' ');

                                if (arg.EndsWith('"'))
                                {
                                    _properties.Add(previousFlagOrProp.ToLower(), spacedProp.ToString());

                                    spacedProp.Clear();
                                    spacedProp = null;
                                }
                            }
                            else
                            {
                                _properties.Add(previousFlagOrProp.ToLower(), arg);

                                previousFlagOrProp = null;
                            }
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
            catch (Exception ex)
            {
                throw new ArgumentException("Argument parser excpetion. Provided argument format is invalid.", ex);
            }
        }

        public bool IsHelp => _helpInvoked;
        public int Count => _count;
        public bool Any => _count > 0;
        public string GetModuleSelector => _moduleSelector;
        public string GetSubmoduleSelector => _submoduleSelector;
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

        private string GetSubmoduleOrDefault()
        {
            if (CommandCount < 2) return null;

            return _commands.Skip(1).FirstOrDefault();
        }
    }
}
