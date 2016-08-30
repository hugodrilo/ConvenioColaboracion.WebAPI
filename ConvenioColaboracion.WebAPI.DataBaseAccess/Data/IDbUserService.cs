//-----------------------------------------------------------------------
// <copyright file="IDbUserService.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    using ConvenioColaboracion.WebAPI.Entities.Models.Return;
    using Entities.Models.Request;

    /// <summary>
    /// The database user service interface.
    /// </summary>
    public interface IDbUserService
    {
        /// <summary>
        /// Gets the user from the database that matches the specified request. 
        /// </summary>
        /// <param name="request">The user request model.</param>
        /// <returns>Expected user model.</returns>
        User GetUser(EUser request);
    }
}
