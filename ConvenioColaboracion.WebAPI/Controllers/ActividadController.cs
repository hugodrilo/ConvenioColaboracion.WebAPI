//-----------------------------------------------------------------------
// <copyright file="ActividadController.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using ConvenioColaboracion.WebAPI.Core.Utilities;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Data;
    using ConvenioColaboracion.WebAPI.Entities.Models.Request;

    /// <summary>
    /// The area controller implementation class.
    /// </summary>
    public class ActividadController : ApiController
    {
        /// <summary>
        /// Gets or sets the database ACTIVIDAD service.
        /// </summary>
        public IDbActividadService DbActividadService { get; set; }

        /// <summary>
        /// Gets or sets the File Manager Utility.
        /// </summary>
        public IFileManagerUtility FileManagerUtility { get; set; }

        /// <summary>
        /// Gets a single ACTIVIDAD model.
        /// </summary>
        /// <param name="id">The ACTIVIDAD identifier</param>
        /// <returns>Expected ACTIVIDAD MODEL.</returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            // Call the data service
            var actividad = this.DbActividadService.Get(id);

            if (actividad == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontraron actividades.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, actividad);
        }

        /// <summary>
        /// Gets a single ACTIVIDAD model.
        /// </summary>
        /// <param name="id">The COMPROMISO identifier</param>
        /// <returns>Expected ACTIVIDAD MODEL.</returns>
        [HttpGet]
        public HttpResponseMessage GetAllById(int id)
        {
            // Call the data service
            var actividadList = this.DbActividadService.GetAllById(id);

            var actividades = actividadList as EActividad[] ?? actividadList.ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, actividades);
        }

        /// <summary>
        ///  Allows to insert the specified ACTIVIDAD.
        /// </summary>
        /// <param name="actividadRequest">The ACTIVIDAD request model.</param>
        /// <returns>A value indicating whether the data was successful inserted or not.</returns>
        ////[Authorize]
        ////[Authorize(Roles = "Administrators")] //Restrict by roles
        ////[Authorize(Users = "Joydip,Jini")] //Restrict access by user
        [HttpPost]
        public HttpResponseMessage Post([FromBody] EActividad actividadRequest)
        {
            // RutaDocumento = ANEXO
            if (this.ModelState.IsValid)
            {
                if (actividadRequest == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request object.");
                }

                if (!string.IsNullOrWhiteSpace(actividadRequest.Documento))
                {
                    const string CompromisoFolderName = @"Compromiso\";
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    var destinationFolder = baseDirectory + CompromisoFolderName + actividadRequest.CompromisoId;

                    var finalPath = this.FileManagerUtility.GetDocumentPath(actividadRequest.NombreDocumento, destinationFolder);

                    var copied = this.FileManagerUtility.CopyDocument(actividadRequest.Documento, finalPath);

                    if (copied)
                    {
                        actividadRequest.Anexo = finalPath;
                    }
                }

                // Call the data service
                var result = this.DbActividadService.Insert(actividadRequest);

                if (!result)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Actividad no insertada.");
                }

                return Request.CreateResponse(HttpStatusCode.Created, "Actividad insertada.");
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
        }

        /// <summary>
        ///  Allows to update the specified ACTIVIDAD.
        /// </summary>
        /// <param name="actividadRequest">The ACTIVIDAD request model.</param>
        /// <returns>A value indicating whether the data was successful inserted or not.</returns>
        [HttpPut]
        public HttpResponseMessage Put([FromBody] EActividad actividadRequest)
        {
            if (this.ModelState.IsValid)
            {
                if (actividadRequest == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request object.");
                }

                // Update the document.
                if (!string.IsNullOrWhiteSpace(actividadRequest.Documento))
                {
                    const string CompromisoFolderName = @"Compromiso\";
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    var destinationFolder = baseDirectory + CompromisoFolderName + actividadRequest.CompromisoId;

                    var finalPath = this.FileManagerUtility.GetDocumentPath(actividadRequest.NombreDocumento, destinationFolder);

                    var copied = this.FileManagerUtility.CopyDocument(actividadRequest.Documento, finalPath);

                    if (copied)
                    {
                        actividadRequest.Anexo = finalPath;
                    }
                }

                // Call the data service
                var result = this.DbActividadService.Update(actividadRequest);

                if (!result)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Actividad no actualizada.");
                }

                return Request.CreateResponse(HttpStatusCode.Created, "Actividad Actualizada.");
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
        }

        /// <summary>
        ///  Allows to delete the specified ACTIVIDAD.
        /// </summary>
        /// <param name="id">The ACTIVIDAD identifier.</param>
        /// <returns>A value indicating whether the data was successful inserted or not.</returns>
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            if (this.ModelState.IsValid)
            {
                if (id < 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request object.");
                }

                var actividad = new EActividad { ActividadId = id };

                // Call the data service.
                actividad = this.DbActividadService.Get(actividad.ActividadId);

                // Check the data service result.
                var result = this.DbActividadService.Delete(actividad);

                if (!result)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Actividad no eliminada.");
                }

                // Delete the file. 
                if (!string.IsNullOrEmpty(actividad.Documento))
                {
                    this.FileManagerUtility.DeleteFile(actividad.Documento);
                }

                return Request.CreateResponse(HttpStatusCode.Created, "Actividad Eliminada.");
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
        }
    }
}
