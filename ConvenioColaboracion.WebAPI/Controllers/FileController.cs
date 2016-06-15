//-----------------------------------------------------------------------
// <copyright file="FileController.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Controllers
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Data;

    /// <summary>
    /// The file controller class.
    /// </summary>
    public class FileController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONVENIO service.
        /// </summary>
        public IDbConvenioService DbConvenioService { get; set; }

        /// <summary>
        /// Gets or sets the database ACTIVIDAD service.
        /// </summary>
        public IDbActividadService DbActividadService { get; set; }

        /// <summary>
        /// Gets the CONVENIO document file.
        /// </summary>
        /// <param name="id">The requested CONVENIO identifier.</param>
        /// <returns>The expected file.</returns>
        [HttpGet]
        public HttpResponseMessage GetConvenio(int id)
        {
            // Do not allow negative numbers
            if (id < 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request object.");
            }

            // Call the data service
            var convenio = this.DbConvenioService.Get(id);

            // Create the correct route for this controller
            HttpResponseMessage result = null;

            if (!File.Exists(convenio.RutaDocumento))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                var mimeType = System.Net.Mime.MediaTypeNames.Application.Octet;

                if (convenio.RutaDocumento.ToLower().Contains(".docx"))
                {
                    mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                }
                else if (convenio.RutaDocumento.ToLower().Contains(".doc"))
                {
                    mimeType = "application/msword";
                }
                else if (convenio.RutaDocumento.ToLower().Contains(".pdf"))
                {
                    mimeType = "application/pdf";
                }

                // Serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(convenio.RutaDocumento, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = convenio.NombreDocumento;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
                result.Content.Headers.ContentType.MediaType = mimeType;
            }

            return result;
        }

        /// <summary>
        /// Gets the ACTIVIDAD document file.
        /// </summary>
        /// <param name="id">The requested ACTIVIDAD identifier.</param>
        /// <returns>The expected file.</returns>
        [HttpGet]
        public HttpResponseMessage GetActividad(int id)
        {
            // Do not allow negative numbers
            if (id < 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request object.");
            }

            // Call the data service
            var actividad = this.DbActividadService.Get(id);

            // Create the correct route for this controller
            HttpResponseMessage result = null;

            if (!File.Exists(actividad.Anexo))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                var mimeType = System.Net.Mime.MediaTypeNames.Application.Octet;

                if (actividad.Anexo.ToLower().Contains(".docx"))
                {
                    mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                }
                else if (actividad.Anexo.ToLower().Contains(".doc"))
                {
                    mimeType = "application/msword";
                }
                else if (actividad.Anexo.ToLower().Contains(".pdf"))
                {
                    mimeType = "application/pdf";
                }

                // Serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(actividad.Anexo, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = actividad.NombreDocumento;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
                result.Content.Headers.ContentType.MediaType = mimeType;
            }

            return result;
        }
    }
}
