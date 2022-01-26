namespace Balto.Cli.Abstraction
{
    public interface IArguments
    {
        bool IsHelp { get; }
        bool Any { get; }
        int Count { get; }
        string GetModuleSelector { get; }

        bool IsFlagSet(string flagName);
        int FlagCount { get; }
        bool IsCommandUsed(string commandName);
        int CommandCount { get; }
        string GetPropertyValue(string propertyName);
        string GetPropertyValueOrDefault(string propertyName);
        int PropertyCount { get; }
    }
}
