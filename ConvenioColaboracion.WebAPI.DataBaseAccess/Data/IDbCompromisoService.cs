//-----------------------------------------------------------------------
// <copyright file="IDbCompromisoService.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    using Entities.Models.Request;

    /// <summary>
    /// The database COMPROMISO service interface.
    /// </summary>
    public interface IDbCompromisoService
    {
        /// <summary>
        /// Gets the COMPROMISO from the database. 
        /// </summary>
        /// <param name="compromisoId">The COMPROMISO identifier.</param>
        /// <returns>Expected COMPROMISO model.</returns>
        ECompromiso GetCompromiso(int compromisoId);
    }
}
