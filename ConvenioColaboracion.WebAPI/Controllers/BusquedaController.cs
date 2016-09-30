//-----------------------------------------------------------------------
// <copyright file="BusquedaController.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Data;
    using ConvenioColaboracion.WebAPI.Entities.Models.Request;

    /// <summary>
    /// The BUSQUEDA controller implementation class.
    /// </summary>
    public class BusquedaController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONSULTA service.
        /// </summary>
        public IDbConsultaService DbConsultaService { get; set; }

        /// <summary>
        /// Gets or sets the database ESTADISTICA service.
        /// </summary>
        public IDbEstadisticaService DbEstadisticaService { get; set; }

        /// <summary>
        /// Gets a list of CONVENIOS.
        /// </summary>
        /// <param name="searchText">The text to be searched.</param>
        /// <param name="usuarioId">The user identifier.</param>
        /// <param name="operacionId">The operation identifier.</param>
        /// <returns>A list of CONVENIOS.</returns>
        [HttpGet]
        public HttpResponseMessage Get(string searchText, int usuarioId = 0, int operacionId = 1)
        {
            if (usuarioId == 0)
            {
                // Call the data service
                var convenioList = this.DbConsultaService.Search(searchText);

                var convenios = convenioList as EConvenio[] ?? convenioList.ToArray();

                if (!convenios.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No se encontraron convenios para la búsqueda solicitada.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, convenios);
            }
            else
            {
                var request = new EBuscaConvenio();

                request.Filtros = new EFiltrosBusqueda();
                request.Pagina = 1;
                request.Registros = 1000;
                var areaList = new List<int>();
                request.Filtros.Areas = areaList;
                var avanceList = new List<string>();
                request.Filtros.Avances = avanceList;
                request.Filtros.Materias = new List<int>();
                var materiasList = new List<int>();
                request.Filtros.Materias = materiasList;
                request.Filtros.Partes = new List<int>();
                request.Filtros.Periodos = new List<int>();
                var periodosList = new List<int>();
                request.Filtros.Periodos = periodosList;
                request.Filtros.RI = new List<int>();
                request.Filtros.RO = new List<int>();
                request.Keywords = searchText;
                request.UsuarioId = usuarioId;
                request.OperacionId = operacionId;

                // Call the data service
                var convenioList = this.DbEstadisticaService.GetConvenio(request);

                return Request.CreateResponse(HttpStatusCode.OK, convenioList);
            }
        }
    }
}
