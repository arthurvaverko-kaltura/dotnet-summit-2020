using Ingest.Models;
using System.ServiceModel;

#if NET461
using System.ServiceModel.Web;
#endif

namespace Ingest
{
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        [ServiceKnownType(typeof(IngestResponse))]
        #if NET461
        [WebInvoke(Method = "POST", UriTemplate = "Ingest", ResponseFormat = WebMessageFormat.Xml, RequestFormat = WebMessageFormat.Xml)]
        #endif
        IngestResponse IngestData(IngestRequest request);

    }
}
