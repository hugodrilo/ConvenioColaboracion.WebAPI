//-----------------------------------------------------------------------
// <copyright file="IDbCompromisoService.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    using System.Collections.Generic;
    using Entities.Models.Request;

    /// <summary>
    /// The database CONVENIO service interface.
    /// </summary>
    public interface IDbCompromisoService : IDbServiceBase<ECompromiso>
    {
        /// <summary>
        /// Gets all the CONVENIOS from the database. 
        /// </summary>
        /// <returns> Expected list of CONVENIOS.</returns>
        IEnumerable<EConvenio> GetAll();

        /// <summary>
        /// Gets the specified CONVENIO from the database. 
        /// </summary>
        /// <param name="convenioId">The CONVENIO identifier.</param>
        /// <returns>Expected CONVENIO model.</returns>
        EConvenio Get(int convenioId);
    }
}
