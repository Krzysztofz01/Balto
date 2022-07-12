using Balto.Application.Plugin.Core;

namespace Balto.Application.Plugin.TrelloIntegration
{
    public class TrelloIntegrationSettings : BaltoPluginSettingsBase
    {
        private bool _createTagsFromSquareBrackets;
        public bool CreateTagsFromSquareBrackets
        {
            //TODO: Not implemented yet
            //get => _createTagsFromSquareBrackets;
            get => false;
            set => _createTagsFromSquareBrackets = value;
        }
    }
}
