//-----------------------------------------------------------------------
// <copyright file="BusquedaController.cs" company="SFP">
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
    /// The BUSQUEDA controller implementation class.
    /// </summary>
    [Authorize]
    public class BusquedaController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONSULTA service.
        /// </summary>
        public IDbConsultaService DbConsultaService { get; set; }

        /// <summary>
        /// Gets a list of CONVENIOS.
        /// </summary>
        /// <param name="searchText">The text to be searched.</param>
        /// <returns>A list of CONVENIOS.</returns>
        [HttpGet]
        public HttpResponseMessage Get(string searchText)
        {
            // Call the data service
            var convenioList = this.DbConsultaService.Search(searchText);

            var convenios = convenioList as EConvenio[] ?? convenioList.ToArray();

            if (!convenios.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontraron convenios para la búsqueda solicitada.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, convenios);
        }
    }
}
