//-----------------------------------------------------------------------
// <copyright file="SubMateriaController.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Data;
    using ConvenioColaboracion.WebAPI.Entities.Models.Request;

    /// <summary>
    /// The SUBMATERIA controller implementation class.
    /// </summary>
    public class SubMateriaController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONVENIO service.
        /// </summary>
        public IDbConvenioService DbConvenioService { get; set; }

        /// <summary>
        /// Gets a list of MATERIAS.
        /// </summary>
        /// <returns>A list of MATERIAS.</returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            // Call the data service
            var materiaList = this.DbConvenioService.GetSubMateria(id);

            var materias = materiaList as EMateria[] ?? materiaList.ToArray();

            if (!materias.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontraron materias");
            }

            return Request.CreateResponse(HttpStatusCode.OK, materias);
        }
    }
}
