using System;

namespace HttpLayer.Log
{
    public interface ILog
    {
        void WriteInformation(string message, HttpRequest request);
        void WriteError(string message, HttpRequest request, Exception excception);
        void WriteWarning(string message, HttpRequest request, Exception excception = null);
    }
}
