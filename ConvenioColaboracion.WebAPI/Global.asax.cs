//-----------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI
{
    using System.Web.Http;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Data;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Utilities;
    using LightInject;

    /// <summary>
    /// Global Http Application for Web API Project.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Runs on application start.
        /// </summary>
        protected void Application_Start()
        {
            using (var container = new ServiceContainer())
            {
                // Configure depenency injection
                container.RegisterApiControllers();
                container.EnableWebApi(GlobalConfiguration.Configuration);

                // Register all the services and implementations.
                container.Register<IDatabaseHelper, DatabaseHelper>();
                container.Register<IDbConsultaService, DbConsultaService>();
                container.Register<IDbConvenioService, DbConvenioService>();

                // Basic web api setup
                GlobalConfiguration.Configure(WebApiConfig.Register);
            }
        }
    }
}