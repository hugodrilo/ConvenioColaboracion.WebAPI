//-----------------------------------------------------------------------
// <copyright file="IDbConvenioService.cs" company="SFP">
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
    public interface IDbConvenioService : IDbServiceBase<EConvenio>
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

        /// <summary>
        /// Gets all the MATERIAS from the database. 
        /// </summary>
        /// <returns>Expected list of MATERIAS.</returns>
        IEnumerable<EMateria> GetMateria();

        /// <summary>
        /// Gets the area list from the database. 
        /// </summary>
        /// <returns> The list of areas.</returns>
        IEnumerable<EArea> GetArea();

        /// <summary>
        /// Gets the PARTE list from the database. 
        /// </summary>
        /// <returns> The list of PARTES.</returns>
        IEnumerable<EParte> GetParte();
    }
}
