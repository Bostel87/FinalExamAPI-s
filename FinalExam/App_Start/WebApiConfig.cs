using FinalExam.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FinalExam
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();
            //Esta linea Sirve para configurar el Handler del APIKEY
            config.MessageHandlers.Add(new KeyHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
