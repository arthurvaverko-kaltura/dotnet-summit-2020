using Ingest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Ingest
{
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        [ServiceKnownType(typeof(IngestResponse))]
        [WebInvoke(Method = "POST", UriTemplate = "Ingest", ResponseFormat = WebMessageFormat.Xml, RequestFormat = WebMessageFormat.Xml)]
        IngestResponse IngestData(IngestRequest request);

    }
}
