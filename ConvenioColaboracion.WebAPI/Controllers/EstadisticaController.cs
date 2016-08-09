﻿//-----------------------------------------------------------------------
// <copyright file="EstadisticaController.cs" company="SFP">
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

        /// <summary>
        /// Gets a CONVENIO list.
        /// </summary>
        /// <param name="admonId">The ADMON identifier .</param>
        /// <param name="matId">The MATERIA identifier.</param>
        /// <param name="areaId">The area identifier .</param>
        /// <param name="estatusId">The status identifier.</param>
        /// <returns>Expected CONVENIO model list.</returns>
        [HttpGet]
        public HttpResponseMessage GetConvenios(
            int admonId,
            int matId,
            int areaId = 0,
            int estatusId = 0)
        {
            var request = new EBuscaConvenio();

            request.Filtros = new EFiltrosBusqueda();
            request.Pagina = 1;
            request.Registros = 1000;
            request.Keywords = string.Empty;
            var areaList = new List<int>();

            if (areaId > 0)
            {
                areaList.Add(areaId);
            }

            request.Filtros.Areas = areaList;

            var avanceList = new List<string>();

            if (estatusId > 0)
            {
                avanceList.Add(estatusId.ToString());
            }

            request.Filtros.Avances = avanceList;

            request.Filtros.Materias = new List<int>();
            var materiasList = new List<int>();

            if (matId > 0)
            {
                materiasList.Add(matId);
            }

            request.Filtros.Materias = materiasList;
            request.Filtros.Partes = new List<int>();
            request.Filtros.Periodos = new List<int>();
            var periodosList = new List<int>();

            if (admonId > 0)
            {
                periodosList.Add(admonId);
            }

            request.Filtros.Periodos = periodosList;
            request.Filtros.RI = new List<int>();
            request.Filtros.RO = new List<int>();

            // Call the data service
            var convenioList = this.DbEstadisticaService.GetConvenio(request);

            return Request.CreateResponse(HttpStatusCode.OK, convenioList);
        }
    }
}