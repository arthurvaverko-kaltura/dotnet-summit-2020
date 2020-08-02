using KLogMonitor;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ApiApp
{
    internal class LoggingContextHandler : DelegatingHandler
    {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpContext.Current.Items[KLogger.REQUEST_ID_KEY] = request.GetCorrelationId();
            return base.SendAsync(request, cancellationToken);
        }
    }
}