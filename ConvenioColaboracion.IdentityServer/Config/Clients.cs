//-----------------------------------------------------------------------
// <copyright file="Clients.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.IdentityServer.Config
{
    using System.Collections.Generic;
    using IdentityServer3.Core.Models;

    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>();
        }
    }
}