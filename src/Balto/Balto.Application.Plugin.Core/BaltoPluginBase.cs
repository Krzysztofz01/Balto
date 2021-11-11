namespace Balto.Application.Plugin.Core
{
    public abstract class BaltoPluginBase
    {
        protected abstract string PluginName { get; }
        protected abstract string PluginDescription { get; }
        protected abstract string PluginVersion { get; }
    }
}
