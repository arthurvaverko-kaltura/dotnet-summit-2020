using Microsoft.AspNetCore.Http;
using System;

/// <summary>
/// This is a shim for system web namespace to allow .net core apps to access HttpContext
/// This is inspired by https://www.strathweb.com/2016/12/accessing-httpcontext-outside-of-framework-components-in-asp-net-core/
/// </summary>
namespace System.Web
{
    public class HttpContext
    {
        private static IHttpContextAccessor _contextAccessor;

        public static Microsoft.AspNetCore.Http.HttpContext Current => _contextAccessor?.HttpContext;

        public static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }
}
