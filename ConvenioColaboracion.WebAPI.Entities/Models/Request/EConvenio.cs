//-----------------------------------------------------------------------
// <copyright file="EConvenio.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The CONVENIO request model.
    /// </summary>
    public class EConvenio
    {
        /// <summary>
        /// Gets or sets the CONVENIO identifier.
        /// </summary>
        /// <value>The CONVENIO identifier.</value>
        public int ConvenioId { get; set; }

        /// <summary>
        /// Gets or sets the NUMERO CONVENIO.
        /// </summary>
        /// <value> The NUMERO CONVENIO.</value>
        [Required(ErrorMessage = "NumeroConvenio is required.")]
        public string NumeroConvenio { get; set; }

        /// <summary>
        /// Gets or sets the CONVENIO text.
        /// </summary>
        /// <value> The CONVENIO text.</value>
        [Required(ErrorMessage = "Convenio is required.")]
        public string Convenio { get; set; }

        /// <summary>
        /// Gets or sets the NOMBRE CORTO.
        /// </summary>
        /// <value> The NOMBRE CORTO.</value>
        public string NombreCorto { get; set; }

        /// <summary>
        /// Gets or sets the RESUMEN.
        /// </summary>
        /// <value> The RESUMEN.</value>
        public string Resumen { get; set; }

        /// <summary>
        /// Gets or sets the FECHA SUSCRIPCION.
        /// </summary>
        /// <value> The FECHA SUSCRIPCION.</value>
        [Required(ErrorMessage = "FechaSuscripcion is required.")]
        public string FechaSuscripcion { get; set; }

        /// <summary>
        /// Gets or sets the FECHA TERMINO.
        /// </summary>
        /// <value> The FECHA TERMINO.</value>
        public string FechaTermino { get; set; }

        /// <summary>
        /// Gets or sets the MATERIA.
        /// </summary>
        /// <value>The MATERIA model.</value>
        public EMateria Materia { get; set; }

        /// <summary>
        /// Gets or sets the SUBMATERIA.
        /// </summary>
        /// <value>The MATERIA model.</value>
        public EMateria SubMateria { get; set; }

        /// <summary>
        /// Gets or sets the SEXENIO identifier.
        /// </summary>
        /// <value>The SEXENIO identifier.</value>
        public int? SexenioId { get; set; }

        /// <summary>
        /// Gets or sets the AÑO identifier.
        /// </summary>
        /// <value>The AÑO identifier.</value>
        public int? Año { get; set; }

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

        /// <summary>
        /// Gets or sets the FECHA ULTIMA ACTIVIDAD.
        /// </summary>
        /// <value> The FECHA ULTIMA ACTIVIDAD.</value>
        public DateTime? FechaUltimaActividad { get; set; }

        /// <summary>
        /// Gets or sets the AVANCE.
        /// </summary>
        /// <value> The AVANCE.</value>
        public int? Avance { get; set; }

        /// <summary>
        /// Gets or sets the ESTATUS.
        /// </summary>
        /// <value> The ESTATUS.</value>
        public char? Estatus { get; set; }

        /// <summary>
        /// Gets or sets the DOCUMENTO.
        /// </summary>
        /// <value> The DOCUMENTO.</value>
        public string Documento { get; set; }

        /// <summary>
        /// Gets or sets the NOMBRE DOCUMENTO.
        /// </summary>
        /// <value> The NOMBRE DOCUMENTO.</value>
        public string NombreDocumento { get; set; }

        /// <summary>
        /// Gets or sets the RUTA DOCUMENTO on the server.
        /// </summary>
        /// <value> The RUTA DOCUMENTO on the server.</value>
        public string RutaDocumento { get; set; }

        /// <summary>
        /// Gets or sets the COMENTARIOS.
        /// </summary>
        /// <value> The COMENTARIOS.</value>
        public string Comentarios { get; set; }

        /// <summary>
        /// Gets or sets the CONVENIO AREA.
        /// </summary>
        /// <value> The CONVENIO AREA list.</value>
        public IEnumerable<EConvenioArea> Areas { get; set; }

        /// <summary>
        /// Gets or sets the CONVENIO PARTE.
        /// </summary>
        /// <value> The CONVENIO PARTE list.</value>
        public IEnumerable<EConvenioParte> Partes { get; set; }

        /// <summary>
        /// Gets or sets the COMPROMISOS.
        /// </summary>
        /// <value> The COMPROMISOS list.</value> 
        public IEnumerable<ECompromiso> Compromisos { get; set; }

        /// <summary>
        /// Gets or sets the PARTES COMPROMISO model.
        /// </summary>
        /// <value>The PARTES COMPROMISO list.</value>
        public IEnumerable<EParte> PartesCompromiso { get; set; }
    }
}
