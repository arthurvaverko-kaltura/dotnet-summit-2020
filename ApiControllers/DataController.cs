using ApiModels;
using DAL;
using KLogMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;


#if NETCOREAPP3_1
using Microsoft.AspNetCore.Mvc;
#else
using System.Web.Http;
#endif

namespace ApiControllers
{
    public class DataController : BaseApiController
    {

        private static readonly KLogger _Logger = new KLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        [HttpGet]
        public IEnumerable<User> Get()
        {
            _Logger.Info("Getting Values...");
            return UsersManager.GetUsers();
        }
    }
}
