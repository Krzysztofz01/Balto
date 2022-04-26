using Balto.Application.Plugin.Core;

namespace Balto.Application.Plugin.TrelloIntegration
{
    public class TrelloIntegrationSettings : BaltoPluginSettingsBase
    {
        public bool CreateTagsFromSquareBrackets { get; set; }
    }
}
