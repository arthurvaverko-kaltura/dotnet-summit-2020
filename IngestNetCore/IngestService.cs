using Ingest.Models;
using KLogMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IngestNetCore
{
    public class IngestService : Ingest.IService
    {
        private static readonly KLogger _Logger = new KLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        public IngestResponse IngestData(IngestRequest request)
        {
            _Logger.Info("Handling ingest from net core");
            return new IngestResponse
            {
                Status = 0,
            };
        }
    }
}

