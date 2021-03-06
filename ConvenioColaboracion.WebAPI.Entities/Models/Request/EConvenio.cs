﻿//-----------------------------------------------------------------------
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
        [Required(ErrorMessage = "ConvenioId is required.")]
        [Display(Name = "ConvenioId")]
        public int ConvenioId { get; set; }

        /// <summary>
        /// Gets or sets the NUMERO CONVENIO.
        /// </summary>
        /// <value> The NUMERO CONVENIO.</value>
        public string NumeroConvenio { get; set; }

        /// <summary>
        /// Gets or sets the CONVENIO text.
        /// </summary>
        /// <value> The CONVENIO text.</value>
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
        public DateTime? FechaSuscripcion { get; set; }

        /// <summary>
        /// Gets or sets the FECHA TERMINO.
        /// </summary>
        /// <value> The FECHA TERMINO.</value>
        public DateTime? FechaTermino { get; set; }

        /// <summary>
        /// Gets or sets the MATERIA identifier.
        /// </summary>
        /// <value>The MATERIA identifier.</value>
        public int? MateriaId { get; set; }

        /// <summary>
        /// Gets or sets the SUBMATERIA identifier.
        /// </summary>
        /// <value>The SUBMATERIA identifier.</value>
        public int? SubmateriaId { get; set; }

        /// <summary>
        /// Gets or sets the AREA VINCULANTE identifier.
        /// </summary>
        /// <value>The AREA VINCULANTE identifier.</value>
        public int? AreaVinculanteId { get; set; }

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
        /// Gets or sets the RUTA DOCUMENTO.
        /// </summary>
        /// <value> The RUTA DOCUMENTO.</value>
        public string RutaDocumento { get; set; }

        /// <summary>
        /// Gets or sets the PARTES.
        /// </summary>
        /// <value> The PARTES list.</value>
        public IEnumerable<EParte> Partes { get; set; }

        /// <summary>
        /// Gets or sets the COMPROMISOS.
        /// </summary>
        /// <value> The COMPROMISOS list.</value>
        public IEnumerable<ECompromiso> Compromisos { get; set; }
    }
}
