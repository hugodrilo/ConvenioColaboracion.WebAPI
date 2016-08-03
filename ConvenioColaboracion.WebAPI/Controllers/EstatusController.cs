//-----------------------------------------------------------------------
// <copyright file="EstatusController.cs" company="SFP">
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
    /// The ESTATUS controller implementation class.
    /// </summary>
    public class EstatusController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONVENIO service.
        /// </summary>
        public IDbConvenioService DbConvenioService { get; set; }

        /// <summary>
        /// Gets a list of ESTATUS .
        /// </summary>
        /// <returns>A list of ESTATUS.</returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            // Call the data service
            var estatusList = this.DbConvenioService.GetEstatus();

            var estatus = estatusList as EEstatus[] ?? estatusList.ToArray();

            if (!estatus.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontraron estatus (grado de cumplimiento).");
            }

            return Request.CreateResponse(HttpStatusCode.OK, estatus);
        }
    }
}
