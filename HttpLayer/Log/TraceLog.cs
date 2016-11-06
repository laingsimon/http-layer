using System;
using System.Diagnostics;

namespace HttpLayer.Log
{
    public class TraceLog : ILog
    {
        public void WriteError(string message, HttpRequest request, Exception excception)
        {
            Trace.TraceError(message);
        }

        public void WriteInformation(string message, HttpRequest request)
        {
            Trace.TraceInformation(message);
        }

        public void WriteWarning(string message, HttpRequest request, Exception excception = null)
        {
            Trace.TraceWarning(message);
        }
    }
}
