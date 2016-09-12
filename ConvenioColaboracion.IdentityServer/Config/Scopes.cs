//-----------------------------------------------------------------------
// <copyright file="Scopes.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.IdentityServer.Config
{
    using System.Collections.Generic;
    using IdentityServer3.Core.Models;

    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>()
            {
                new Scope()
                {
                    Name = "convenioColaboracion",
                    DisplayName = "Convenios de colaboracion",
                    Description = "Convenios de colaboracion descripcion",
                    Type = ScopeType.Resource
                }
            };
        }
    }
}