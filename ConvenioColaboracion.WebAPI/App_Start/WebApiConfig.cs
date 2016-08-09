//-----------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Handles actions for the WebAPI
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Remove XML Formats
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // setup JSON config
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Add all the custom routes.
            config.Routes.MapHttpRoute(
                name: "Convenio",
                routeTemplate: "api/convenio/{id}",
                defaults: new { controller = "Convenio", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Materia",
                routeTemplate: "api/materia/{id}",
                defaults: new { controller = "Materia", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "SubMateria",
                routeTemplate: "api/submateria/{id}",
                defaults: new { controller = "SubMateria", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Area",
                routeTemplate: "api/area/{id}",
                defaults: new { controller = "Area", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "TipoArea",
                routeTemplate: "api/tipoarea/{id}",
                defaults: new { controller = "TipoArea", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Parte",
                routeTemplate: "api/parte/{id}",
                defaults: new { controller = "Parte", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "File",
                routeTemplate: "api/file/{action}/{id}",
                defaults: new { controller = "File", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Busqueda",
                routeTemplate: "api/busqueda/{searchText}",
                defaults: new { controller = "Busqueda", searchText = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Compromiso",
                routeTemplate: "api/compromiso/{id}",
                defaults: new { controller = "Compromiso", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Actividad",
                routeTemplate: "api/actividad/{action}/{id}",
                defaults: new { controller = "Actividad", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Estatus",
                routeTemplate: "api/estatus/{id}",
                defaults: new { controller = "Estatus", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Estadistica",
                routeTemplate: "api/estadistica/{action}/{id}",
                defaults: new { controller = "Estadistica", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "EstadisticaAdmon",
                routeTemplate: "api/estadistica/{action}/{admonId}/{matId}/{areaId}/{estatusId}",
                defaults:
                    new
                    {
                        controller = "Estadistica",
                        admonId = RouteParameter.Optional,
                        matId = RouteParameter.Optional,
                        areaId = RouteParameter.Optional,
                        estatusId = RouteParameter.Optional
                    });

            config.Routes.MapHttpRoute(
                name: "Alerta",
                routeTemplate: "api/alerta/{action}/{id}",
                defaults: new { controller = "Alerta", id = RouteParameter.Optional });

            // Setup CORS
            var origins = System.Configuration.ConfigurationManager.AppSettings.Get("AllowedOrigins") ?? " *";
            var cors = new EnableCorsAttribute(origins, "*", "*");
            config.EnableCors(cors);
        }
    }
}
