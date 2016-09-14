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

    /// <summary>
    /// The identity server scopes implementation class.
    /// </summary>
    public class Scopes
    {
        /// <summary>
        /// Gets all the scopes.
        /// </summary>
        /// <returns>The expected list of scopes.</returns>
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>()
            {
                StandardScopes.OpenId,
                StandardScopes.ProfileAlwaysInclude,
            ////    StandardScopes.Address,
                new Scope()
                {
                    Name = "convenioColaboracion",
                    DisplayName = "Convenios de colaboracion",
                    Description = "Permite manejar Convenios de Colaboracion.",
                    Type = ScopeType.Resource
                }
            };
            /*return new List<Scope>
            { 
                 StandardScopes.OpenId,
                 StandardScopes.ProfileAlwaysInclude,
                 StandardScopes.Address,                     
                 new Scope
                 { 
                     Name = "gallerymanagement",
                     DisplayName = "Gallery Management",
                     Description = "Allow the application to manage galleries on your behalf.",
                     Type = ScopeType.Resource 
                 }
            };*/
        }
    }
}