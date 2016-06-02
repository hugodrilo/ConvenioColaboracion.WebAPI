//-----------------------------------------------------------------------
// <copyright file="EActividad.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The ACTIVIDAD request model.
    /// </summary>
    public class EActividad
    {
        /// <summary>
        /// Gets or sets the ACTIVIDAD identifier.
        /// </summary>
        /// <value>The ACTIVIDAD identifier.</value>
        public int ActividadId { get; set; }

        /// <summary>
        /// Gets or sets the COMPROMISO identifier.
        /// </summary>
        /// <value>The COMPROMISO identifier.</value>
        public int CompromisoId { get; set; }

        /// <summary>
        /// Gets or sets the FECHA ACTIVIDAD date.
        /// </summary>
        /// <value> The FECHA ACTIVIDAD date.</value>
        [Required(ErrorMessage = "Fecha Actividad is required.")]
        public string FechaActividad { get; set; }

        /// <summary>
        /// Gets or sets the AVANCE percentage.
        /// </summary>
        /// <value>The AVANCE percentage.</value>
        public int? PorcentajeAvance { get; set; }

        /// <summary>
        /// Gets or sets the ACTIVIDAD text.
        /// </summary>
        /// <value> The ACTIVIDAD text.</value>
        [Required(ErrorMessage = "Actividad is required.")]
        public string Actividad { get; set; }

        /// <summary>
        /// Gets or sets the ANEXO text (RUTA DOCUMENTO).
        /// </summary>
        /// <value> The ANEXO text.</value>
        public string Anexo { get; set; }

        /// <summary>
        /// Gets or sets the DOCUMENTO.
        /// </summary>
        /// <value> The DOCUMENTO.</value>
        public string Documento { get; set; }

        /// <summary>
        /// Gets or sets the NOMBRE DOCUMENTO text.
        /// </summary>
        /// <value> The NOMBRE DOCUMENTO text.</value>
        public string NombreDocumento { get; set; }

        /// <summary>
        /// Gets or sets the FECHA CREACION.
        /// </summary>
        /// <value> The FECHA CREACION.</value>
        public DateTime? FechaCreacion { get; set; }

        /// <summary>
        /// Gets or sets the FECHA ACTUALIZACION.
        /// </summary>
        /// <value> The FECHA ACTUALIZACION.</value>
        public DateTime? FechaActualizacion { get; set; }

        /// <summary>
        /// Gets or sets the USUARIO CREACION.
        /// </summary>
        /// <value> The USUARIO CREACION.</value>
        public string UsuarioCreacion { get; set; }

        /// <summary>
        /// Gets or sets the USUARIO ACTUALIZACION.
        /// </summary>
        /// <value> The USUARIO ACTUALIZACION.</value>
        public string UsuarioActualizacion { get; set; }
    }
}
