using System.Web.Http.Filters;

namespace ApiApp.Filters
{
    internal class LoggingContextHandler : IFilter
    {
        public LoggingContextHandler()
        {
        }

        public bool AllowMultiple => throw new System.NotImplementedException();
    }
}