//-----------------------------------------------------------------------
// <copyright file="AreaController.cs" company="SFP">
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
    [Authorize]
    public class AreaController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONVENIO service.
        /// </summary>
        public IDbConvenioService DbConvenioService { get; set; }

        /// <summary>
        /// Gets a list of areas.
        /// </summary>
        /// <returns>A list of areas.</returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            // Call the data service
            var areaList = this.DbConvenioService.GetArea();

            var areas = areaList as EArea[] ?? areaList.ToArray();

            if (!areas.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontraron areas.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, areas);
        }
    }
}
