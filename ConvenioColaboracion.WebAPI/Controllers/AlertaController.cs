//-----------------------------------------------------------------------
// <copyright file="AlertaController.cs" company="SFP">
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
    /// The ALERTA controller implementation class.
    /// </summary>
    public class AlertaController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONVENIO service.
        /// </summary>
        public IDbAlertaService DbAlertaService { get; set; }

        /// <summary>
        /// Gets a list of inactive alerts.
        /// </summary>
        /// <returns>A list of alerts.</returns>
        [HttpGet]
        public HttpResponseMessage GetInactividad()
        {
            var request = new EAlerta();

            request.Pagina = 1;
            request.Registro = 1000;
            request.Meses = 3;

            // Call the data service
            var alertaList = this.DbAlertaService.GetAlertaInactividad(request);

            var alertas = alertaList as Entities.Models.Return.EAlerta[] ?? alertaList.ToArray();

            if (!alertas.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontraron alertas inactivas.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, alertas);
        }

        /// <summary>
        /// Gets a list of VENCIMIENTO alerts.
        /// </summary>
        /// <returns>A list of alerts.</returns>
        [HttpGet]
        public HttpResponseMessage GetVencimiento()
        {
            var request = new EAlerta();

            request.Pagina = 1;
            request.Registro = 1000;
            request.Meses = 3;

            // Call the data service
            var alertaList = this.DbAlertaService.GetAlertaInactividad(request);

            var alertas = alertaList as Entities.Models.Return.EAlerta[] ?? alertaList.ToArray();

            if (!alertas.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontraron alertas proximas a vencer.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, alertas);
        }
    }
}
