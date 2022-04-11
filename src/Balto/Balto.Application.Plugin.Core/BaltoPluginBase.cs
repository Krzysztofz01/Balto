namespace Balto.Application.Plugin.Core
{
    public abstract class BaltoPluginBase<TPlugin> where TPlugin : BaltoPluginBase<TPlugin>
    {
        protected abstract string PluginName { get; }
        protected abstract string PluginDescription { get; }
        protected abstract string PluginVersion { get; }
    }
}
