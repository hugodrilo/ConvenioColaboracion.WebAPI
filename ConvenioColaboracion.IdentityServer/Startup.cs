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
    using IdentityServer3.Core.Configuration;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idsrvApp =>
            {
                var idServerServiceFactory = new IdentityServerServiceFactory()
                                .UseInMemoryClients(Clients.Get())
                                .UseInMemoryScopes(Scopes.Get())
                                .UseInMemoryUsers(Users.Get());

                var options = new IdentityServerOptions
                {
                    // TODO: Move this to a global resource
                    Factory = idServerServiceFactory,
                    SiteName = "ConvenioColaboracion Security Token Service",
                    IssuerUri = "https://localhost:44391/identity", ////TripGallery.Constants.TripGalleryIssuerUri,
                    PublicOrigin = "https://localhost:44391/",
                    SigningCertificate = LoadCertificate()
                };

                idsrvApp.UseIdentityServer(options);
            });
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\certificates\idsrv3test.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}