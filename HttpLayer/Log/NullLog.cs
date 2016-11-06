using System;

namespace HttpLayer.Log
{
    internal class NullLog : ILog
    {
        public void WriteError(string message, HttpRequest request, Exception excception)
        { }

        public void WriteInformation(string message, HttpRequest request)
        { }

        public void WriteWarning(string message, HttpRequest request, Exception excception = null)
        { }
    }
}
