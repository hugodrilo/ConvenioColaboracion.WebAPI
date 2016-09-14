//-----------------------------------------------------------------------
// <copyright file="Clients.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.IdentityServer.Config
{
    using System.Collections.Generic;
    using ConvenioColaboracion.WebAPI.Core.Resources;
    using IdentityServer3.Core.Models;

    /// <summary>
    /// The identity server clients implementation class.
    /// </summary>
    public class Clients
    {
        /// <summary>
        /// Gets all the clients.
        /// </summary>
        /// <returns>The expected list of clients.</returns>
        public static IEnumerable<Client> Get()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "convenioImplicit",
                    ClientName = "ConvenioColaboracionImplicit",
                    Flow = Flows.Implicit,
                    AllowAccessToAllScopes = true,
                    // redirect = URI of the Angular application
                    RedirectUris = new List<string>()
                    {
                        Constants.ConvenioAngular + "callback.html"
                    }
                }
            };
        }
    }
}