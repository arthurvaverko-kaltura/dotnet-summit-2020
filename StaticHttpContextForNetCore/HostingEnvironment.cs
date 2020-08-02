using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;

/// <summary>
/// This is a shim for systme web namespace to allow .net core apps to access HttpContext
/// This is inspired by https://www.strathweb.com/2016/12/accessing-httpcontext-outside-of-framework-components-in-asp-net-core/
/// </summary>
namespace System.Web
{
    public static class HostingEnvironment
    {
        private static IHostingEnvironment _host;
 
        public static IHostingEnvironment Current => _host;


        internal static void Configure(IHostingEnvironment host)
        {
            _host = host;
        }
    }
}
