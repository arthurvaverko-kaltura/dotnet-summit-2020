using Ingest.Models;
using KLogMonitor;
using System.Reflection;
using System.ServiceModel;

namespace Ingest
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]    
    public class Service : IService
    {
        private static readonly KLogger _Logger = new KLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        public IngestResponse IngestData(IngestRequest request)
        {
            _Logger.Info("Got new ingest request ... ");
            return new IngestResponse
            {
                Status = 0
            };
        }
    }
}
