using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCms.Web.Host.MiddleWare
{
    public static class ErrorPage
    {
        
        public static async Task ResponseAsync(HttpResponse response, int statusCode)
        {
            if (statusCode == 404)
            {
                await response.WriteAsync(Page404);
                //response.Redirect("/api/gateway/", true);
            }
            else if (statusCode == 500)
            {
                await response.WriteAsync(Page500);
            }
        }
        private static string Page404 => @"html...we cant't found page";

        private static string Page500 => @"html...server Internal error";
    }
}
