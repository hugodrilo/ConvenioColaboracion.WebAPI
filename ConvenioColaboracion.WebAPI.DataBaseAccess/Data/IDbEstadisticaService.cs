//-----------------------------------------------------------------------
// <copyright file="IDbEstadisticaService.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    using System.Collections.Generic;
    using ConvenioColaboracion.WebAPI.Entities.Models.Request;

    /// <summary>
    /// The database ESTADISTICA service interface.
    /// </summary>
    public interface IDbEstadisticaService
    {
        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected list of ADMINISTRACION.</returns>
        IEnumerable<Entities.Models.Return.EInformePeriodo> GetInformePeriodo(EInformePeriodo request);

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected list of MATERIAS.</returns>
        IEnumerable<Entities.Models.Return.EInformeMateria> GetInformeMateria(EInformeMateria request);

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected list of areas.</returns>
        IEnumerable<Entities.Models.Return.EInformeArea> GetInformeArea(EInformeArea request);

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected list of GRADO CUMPLIMIENTO.</returns>
        IEnumerable<Entities.Models.Return.EInformeGrado> GetInformeGrado(EInformeGrado request);

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="id">The SEXENIO identifier.</param>
        /// <returns>Expected SEXENIO model.</returns>
        ESexenio GetSexenio(int id);

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected CONVENIO list.</returns>
        IEnumerable<Entities.Models.Return.EBuscaConvenio> GetConvenio(EBuscaConvenio request);
    }
}
