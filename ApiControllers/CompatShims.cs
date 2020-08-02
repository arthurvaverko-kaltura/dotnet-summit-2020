using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/// <summary>
/// This shim is here to avoid using the #if directives for every using statmen in all controllers
/// We add the missing System.Web.Http to the net core namespace so it wont complain.
/// And we add Microsoft.AspNetCore.Mvc namespace to netFramework so it wont complain.
/// These tow are required for each to allow HttpGet , Post etc.. attributes.
/// </summary>
#if NETCOREAPP3_1
namespace System.Web.Http { }
#else

namespace Microsoft.AspNetCore.Mvc { }
#endif
