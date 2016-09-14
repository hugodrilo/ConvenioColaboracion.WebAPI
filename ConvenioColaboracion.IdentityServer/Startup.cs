//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.IdentityServer
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using ConvenioColaboracion.IdentityServer.Config;
    using ConvenioColaboracion.WebAPI.Core.Resources;
    using IdentityServer3.Core.Configuration;
    using IdentityServer3.Core.Services.Default;
    using Owin;

    /// <summary>
    /// The startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes configuration values.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public void Configuration(IAppBuilder app)
        {
            app.Map(
                "/identity",
                idsrvApp =>
                {
                    var corsPolicyService = new DefaultCorsPolicyService()
                    {
                        AllowAll = true
                    };

                    var idServerServiceFactory = new IdentityServerServiceFactory()
                        .UseInMemoryClients(Clients.Get())
                        .UseInMemoryScopes(Scopes.Get())
                        .UseInMemoryUsers(Users.Get());

                    idServerServiceFactory.CorsPolicyService = new
                        Registration<IdentityServer3.Core.Services.ICorsPolicyService>(corsPolicyService);

                    var options = new IdentityServerOptions
                    {
                        Factory = idServerServiceFactory,
                        SiteName = "Convenio Colaboracion Security Token Service",
                        IssuerUri = Constants.ConvenioIssuerUri,
                        PublicOrigin = Constants.ConvenioStsOrigin,
                        SigningCertificate = LoadCertificate()
                    };

                    idsrvApp.UseIdentityServer(options);
                });
        }

        /*  public void Configuration(IAppBuilder app)        {
            app.Map("/identity", idsrvApp =>
            {
                var corsPolicyService = new DefaultCorsPolicyService()
                {
                    AllowAll = true
                };
             
                var idServerServiceFactory = new IdentityServerServiceFactory()
                                .UseInMemoryClients(Clients.Get())
                                .UseInMemoryScopes(Scopes.Get())
                                .UseInMemoryUsers(Users.Get());

                idServerServiceFactory.CorsPolicyService = new
                    Registration<IdentityServer3.Core.Services.ICorsPolicyService>(corsPolicyService);

                var options = new IdentityServerOptions
                {
                    Factory = idServerServiceFactory,
                    SiteName = "TripCompany Security Token Service",
                    SigningCertificate = LoadCertificate(),
                    IssuerUri = TripGallery.Constants.TripGalleryIssuerUri,
                    PublicOrigin = TripGallery.Constants.TripGallerySTSOrigin                 
                };

                idsrvApp.UseIdentityServer(options);
            });
        }*/

        /// <summary>
        /// Gets the certificate.
        /// </summary>
        /// <returns>Expected certificate.</returns>
        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(
                    @"{0}\certificates\idsrv3test.pfx",
                    AppDomain.CurrentDomain.BaseDirectory),
                "idsrv3test");
        }
    }
}