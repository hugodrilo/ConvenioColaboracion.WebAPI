//-----------------------------------------------------------------------
// <copyright file="EstadisticaController.cs" company="SFP">
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
    public class EstadisticaController : ApiController
    {
        /// <summary>
        /// Gets or sets the database ESTADISTICA service.
        /// </summary>
        public IDbEstadisticaService DbEstadisticaService { get; set; }

        /// <summary>
        /// Gets a list of INFORMES POR PERIODO.
        /// </summary>
        /// <returns>A list of INFORMES POR PERIODO.</returns>
        [HttpGet]
        public HttpResponseMessage GetInformePeriodo()
        {
            var informeRequest = new EInformePeriodo();
            informeRequest.AgruparAdministracion = 1;

            // Call the data service
            var informeList = this.DbEstadisticaService.GetInformePeriodo(informeRequest);

            var informePeriodo = informeList as Entities.Models.Return.EInformePeriodo[] ?? informeList.ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, informePeriodo);
        }

        /// <summary>
        /// Gets a list of INFORMES POR MATERIA.
        /// </summary>
        /// <returns>A list of INFORMES POR MATERIA.</returns>
        [HttpGet]
        public HttpResponseMessage GetInformeMateria()
        {
            var informeRequest = new EInformeMateria();
            informeRequest.AgruparMateria = 1;

            // Call the data service
            var informeList = this.DbEstadisticaService.GetInformeMateria(informeRequest);

            var informePeriodo = informeList as Entities.Models.Return.EInformeMateria[] ?? informeList.ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, informePeriodo);
        }

        /// <summary>
        /// Gets a list of INFORMES POR MATERIA.
        /// </summary>
        /// <param name="id">The MATERIA identifier.</param>
        /// <returns>A list of INFORMES POR MATERIA.</returns>
        [HttpGet]
        public HttpResponseMessage GetInformeMateria(int id)
        {
            var informeRequest = new EInformeMateria();
            informeRequest.AgruparAdministracion = 1;
            informeRequest.AgruparMateria = 1;
            informeRequest.AdministracionId = id;

            // Call the data service
            var informeList = this.DbEstadisticaService.GetInformeMateria(informeRequest);

            var informePeriodo = informeList as Entities.Models.Return.EInformeMateria[] ?? informeList.ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, informePeriodo);
        }

        /// <summary>
        /// Gets a list of INFORMES POR MATERIA.
        /// </summary>
        /// <returns>A list of INFORMES POR MATERIA.</returns>
        [HttpGet]
        public HttpResponseMessage GetInformeArea()
        {
            var informeRequest = new EInformeArea();
            ////    informeRequest.AgruparMateria = 1;

            // Call the data service
            var informeList = this.DbEstadisticaService.GetInformeArea(informeRequest);

            var informePeriodo = informeList as Entities.Models.Return.EInformeArea[] ?? informeList.ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, informePeriodo);
        }

        /// <summary>
        /// Gets a list of INFORMES POR MATERIA.
        /// </summary>
        /// <returns>A list of INFORMES POR MATERIA.</returns>
        [HttpGet]
        public HttpResponseMessage GetInformeGrado()
        {
            var informeRequest = new EInformeGrado();
            ////informeRequest.AgruparMateria = 1;

            // Call the data service
            var informeList = this.DbEstadisticaService.GetInformeGrado(informeRequest);

            var informePeriodo = informeList as Entities.Models.Return.EInformeGrado[] ?? informeList.ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, informePeriodo);
        }

        /// <summary>
        /// Gets a SEXENIO model.
        /// </summary>
        /// <param name="id">The SEXENIO identifier.</param>
        /// <returns>Expected SEXENIO MODEL.</returns>
        [HttpGet]
        public HttpResponseMessage GetSexenio(int id)
        {
            // Call the data service
            var sexenio = this.DbEstadisticaService.GetSexenio(id);

            return Request.CreateResponse(HttpStatusCode.OK, sexenio);
        }
    }
}
