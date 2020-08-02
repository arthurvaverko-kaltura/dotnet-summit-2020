using ApiApp.DAL;
using ApiApp.Models;
using KLogMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace ApiApp.Controllers
{
    public class DataController : ApiController
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
