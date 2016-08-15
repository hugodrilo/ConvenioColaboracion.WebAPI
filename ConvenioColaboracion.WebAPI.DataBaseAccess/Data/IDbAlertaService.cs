//-----------------------------------------------------------------------
// <copyright file="IDbAlertaService.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    using System.Collections.Generic;
    using ConvenioColaboracion.WebAPI.Entities.Models.Request;

    /// <summary>
    /// The database ALERTA service interface.
    /// </summary>
    public interface IDbAlertaService
    {
        /// <summary>
        /// Gets all the ALERTAS from the database that are inactive for a period of time. 
        /// </summary>
        /// <param name="alertaRequest">The ALERTA request model.</param>
        /// <returns>Expected list of ALERTAS.</returns>
        IEnumerable<Entities.Models.Return.EAlerta> GetAlertaInactividad(EAlerta alertaRequest);

        /// <summary>
        /// Gets all the ALERTAS from the database that are going to expire. 
        /// </summary>
        /// <param name="alertaRequest">The ALERTA request model.</param>
        /// <returns>Expected list of ALERTAS.</returns>
        IEnumerable<Entities.Models.Return.EAlerta> GetAlertaVencimiento(EAlerta alertaRequest);
    }
}
