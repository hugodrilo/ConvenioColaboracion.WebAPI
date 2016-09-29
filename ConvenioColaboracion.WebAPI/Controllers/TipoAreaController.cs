//-----------------------------------------------------------------------
// <copyright file="TipoAreaController.cs" company="SFP">
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
    /// The area controller implementation class.
    /// </summary>
    public class TipoAreaController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONVENIO service.
        /// </summary>
        public IDbConvenioService DbConvenioService { get; set; }

        /// <summary>
        /// Gets a list of TIPO areas.
        /// </summary>
        /// <returns>A list of TIPO areas.</returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            // Call the data service
            var tipoAreaList = this.DbConvenioService.GetTipoArea();

            var tipoAarea = tipoAreaList as ETipoArea[] ?? tipoAreaList.ToArray();

            if (!tipoAarea.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, "No se encontraron tipo de areas");
            }

            return Request.CreateResponse(HttpStatusCode.OK, tipoAarea);
        }
    }
}
