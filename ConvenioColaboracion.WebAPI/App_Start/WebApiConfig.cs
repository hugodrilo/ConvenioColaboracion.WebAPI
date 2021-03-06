﻿//-----------------------------------------------------------------------
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

            //// // Add the default route.
            //// config.Routes.MapHttpRoute(
            ////    name: "DefaultApi",
            ////    routeTemplate: "api/{controller}/{id}",
            ////    defaults: new { id = RouteParameter.Optional });

            // Setup CORS
            var origins = System.Configuration.ConfigurationManager.AppSettings.Get("AllowedOrigins") ?? " *";
            var cors = new EnableCorsAttribute(origins, "*", "*");
            config.EnableCors(cors);
        }
    }
}
