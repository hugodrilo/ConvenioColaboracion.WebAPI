//-----------------------------------------------------------------------
// <copyright file="IDbConsultaService.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    using System.Collections.Generic;
    using Entities.Models.Request;

    /// <summary>
    /// The database CONSULTA service interface.
    /// </summary>
    public interface IDbConsultaService
    {
        /// <summary>
        /// Gets all the CONVENIOS from the database that matches the specified text. 
        /// </summary>
        /// <param name="searchText">The text to search</param>
        /// <param name="usuarioId">The user identifier.</param>
        /// <param name="operacionId">The operation identifier.</param>
        /// <returns>Expected list of CONVENIOS.</returns>
        IEnumerable<EConvenio> Search(string searchText, int usuarioId = 0, int operacionId = 1);
    }
}
