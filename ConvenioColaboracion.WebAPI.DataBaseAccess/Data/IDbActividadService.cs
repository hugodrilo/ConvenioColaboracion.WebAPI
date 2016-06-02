//-----------------------------------------------------------------------
// <copyright file="IDbActividadService.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    using System.Collections.Generic;
    using Entities.Models.Request;

    /// <summary>
    /// The database ACTIVIDAD service interface.
    /// </summary>
    public interface IDbActividadService : IDbServiceBase<EActividad>
    {
        /// <summary>
        /// Gets the specified ACTIVIDAD from the database. 
        /// </summary>
        /// <param name="id">The ACTIVIDAD identifier.</param>
        /// <returns>Expected ACTIVIDAD model.</returns>
        EActividad Get(int id);

        /// <summary>
        /// Gets the specified ACTIVIDAD list from the database. 
        /// </summary>
        /// <param name="compromisoId">The COMPROMISO identifier.</param>
        /// <returns>Expected ACTIVIDAD model list.</returns>
        IEnumerable<EActividad> GetAllById(int compromisoId);
    }
}
