//-----------------------------------------------------------------------
// <copyright file="ConvenioController.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Data;
    using ConvenioColaboracion.WebAPI.Entities.Models.Request;

    /// <summary>
    /// The CONVENIO controller implementation class.
    /// </summary>
    public class ConvenioController : ApiController
    {
        /// <summary>
        /// Gets or sets the database CONVENIO service.
        /// </summary>
        public IDbConvenioService DbConvenioService { get; set; }

        /// <summary>
        /// Gets a list of CONVENIOS.
        /// </summary>
        /// <returns>A list of CONVENIOS.</returns>
        [ActionName("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            // Call the data service
            var convenioList = this.DbConvenioService.GetAll();

            var convenios = convenioList as EConvenio[] ?? convenioList.ToArray();

            if (!convenios.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontraron convenios");
            }

            return Request.CreateResponse(HttpStatusCode.OK, convenios);
        }

        /// <summary>
        /// Gets the specified CONVENIO.
        /// </summary>
        /// <param name="id">The CONVENIO identifier.</param>
        /// <returns>The expected CONVENIO model.</returns>
        [ActionName("Get")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            // Do not allow negative numbers
            if (id < 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request object.");
            }

            // Call the data service
            var convenio = this.DbConvenioService.Get(id);

            if (convenio.ConvenioId == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Convenio no encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, convenio);
        }

        /// <summary>
        ///  Allows to insert the specified CONVENIO.
        /// </summary>
        /// <param name="convenioRequest">The CONVENIO request model.</param>
        /// <returns>A value indicating whether the data was successful inserted or not.</returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] EConvenio convenioRequest)
        {
            if (convenioRequest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request object.");
            }

            if (!string.IsNullOrWhiteSpace(convenioRequest.Documento))
            {
                var finalPath = GetDocumentPath(convenioRequest.NombreDocumento);

                var copied = CopyDocument(convenioRequest.Documento, finalPath);

                convenioRequest.RutaDocumento = finalPath;
            }

            // Call the data service
            var result = this.DbConvenioService.Insert(convenioRequest);

            if (!result)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Convenio no insertado.");
            }

            return Request.CreateResponse(HttpStatusCode.Created, "Convenio insertado.");
        }

        /// <summary>
        ///  Allows to update the specified CONVENIO.
        /// </summary>
        /// <param name="convenioRequest">The CONVENIO request model.</param>
        /// <returns>A value indicating whether the data was successful inserted or not.</returns>
        [HttpPut]
        public HttpResponseMessage Put([FromBody] EConvenio convenioRequest)
        {
            if (convenioRequest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request object.");
            }

            // Call the data service
            var result = this.DbConvenioService.Update(convenioRequest);

            if (!result)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Convenio no actualizado.");
            }

            return Request.CreateResponse(HttpStatusCode.Created, "Convenio Actualizado.");
        }

        /// <summary>
        ///  Allows to delete the specified CONVENIO.
        /// </summary>
        /// <param name="convenioRequest">The CONVENIO request model.</param>
        /// <returns>A value indicating whether the data was successful inserted or not.</returns>
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody] EConvenio convenioRequest)
        {
            if (convenioRequest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request object.");
            }

            // Call the data service
            var result = this.DbConvenioService.Delete(convenioRequest);

            if (!result)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Convenio no eliminado.");
            }

            return Request.CreateResponse(HttpStatusCode.Created, "Convenio Eliminado.");
        }

        #region AuxiliarMethods
        /// <summary>
        /// Gets the document path.
        /// </summary>
        /// <param name="nombreDocumento">The file name</param>
        /// <returns>The generated document path.</returns>
        private static string GetDocumentPath(string nombreDocumento)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            const string ConvenioFolderName = "Convenio";
            var destinyDirectory = baseDirectory + @"\" + ConvenioFolderName;

            if (!Directory.Exists(destinyDirectory))
            {
                Directory.CreateDirectory(destinyDirectory);
            }

            var destinyFilePath = destinyDirectory + @"\" + nombreDocumento;

            return destinyFilePath;
        }

        /// <summary>
        /// Copies the CONVENIO document to the destiny path.
        /// </summary>
        /// <param name="document">The document string representation.</param>
        /// <param name="finalPath">The destiny file path.</param>
        /// <returns>A value indicating whether the file was copied or not.</returns>
        private static bool CopyDocument(string document, string finalPath)
        {
            var fileCopied = false;

            try
            {
                var myString = document.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var bytes = Convert.FromBase64String(myString[1]);

                using (var ms = new MemoryStream(bytes))
                {
                    using (var file = new FileStream(finalPath, FileMode.Create, FileAccess.Write))
                    {
                        if (file.CanWrite)
                        {
                            // Write to file
                            ms.WriteTo(file);
                            fileCopied = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return fileCopied;
        }
        #endregion
    }
}
