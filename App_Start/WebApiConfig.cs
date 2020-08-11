using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using DataModel.EF;
namespace DemoAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<experience>("experiences");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes
    .Add(new MediaTypeHeaderValue("text/html"));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }

              
            );

            config.Routes.MapHttpRoute(
              name: "get operator using name ",
              routeTemplate: "get-name/{name}",
              defaults: new { controller = "Operator", action = "GetUsingName", id = RouteParameter.Optional }


          );
        }
    }
}
