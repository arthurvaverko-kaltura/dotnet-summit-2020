using ApiModels;
using DAL;
using KLogMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace ApiControllers
{
#if NETCOREAPP3_1
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
#else
    public class BaseApiController : ApiController
#endif
    {

    }
}
