//-----------------------------------------------------------------------
// <copyright file="CompromisoController.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Data;

    /// <summary>
    /// The COMPROMISO controller implementation class.
    /// </summary>
    public class CompromisoController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONVENIO service.
        /// </summary>
        public IDbCompromisoService DbCompromisoService { get; set; }

        /// <summary>
        /// Gets a COMPROMISO from de database.
        /// </summary>
        /// <param name="id">The COMPROMISO identifier.</param>
        /// <returns>Expected COMPROMISO model.</returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            // Call the data service
            var compromiso = this.DbCompromisoService.GetCompromiso(id);

            if (compromiso.CompromisoId == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontró compromiso con id: " + id);
            }

            return Request.CreateResponse(HttpStatusCode.OK, compromiso);
        }
    }
}
