using System;
using System.Runtime.Serialization;

namespace Balto.Application.Plugin.Core
{
    public class PluginException : Exception
    {
        public PluginException() : base() { }
        public PluginException(string message) : base(message) { }
        public PluginException(string message, Exception innerException) : base(message, innerException) { }
        protected PluginException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
