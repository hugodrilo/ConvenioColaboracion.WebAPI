//-----------------------------------------------------------------------
// <copyright file="EParte.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The PARTE request model.
    /// </summary>
    public class EParte
    {
        /// <summary>
        /// Gets or sets the PARTE identifier.
        /// </summary>
        /// <value>The PARTE identifier.</value>
        [Required(ErrorMessage = "parteId is required.")]
        [Display(Name = "ParteId")]
        public int ParteId { get; set; }

        /// <summary>
        /// Gets or sets the PARTE text.
        /// </summary>
        /// <value> The PARTE text.</value>
        public string Parte { get; set; }

        /// <summary>
        /// Gets or sets the REPRESENTANTE text.
        /// </summary>
        /// <value> The REPRESENTANTE text.</value>
        public string Representante { get; set; }

        /// <summary>
        /// Gets or sets the SIGLAS text.
        /// </summary>
        /// <value> The SIGLAS text.</value>
        public string Siglas { get; set; }

        /// <summary>
        /// Gets or sets the PAIS identifier.
        /// </summary>
        /// <value>The PAIS identifier.</value>
        public int? PaisId { get; set; }

        /// <summary>
        /// Gets or sets the ENTIDAD identifier.
        /// </summary>
        /// <value>The ENTIDAD identifier.</value>
        public int? EntidadId { get; set; }

        /// <summary>
        /// Gets or sets the GOBIERNO identifier.
        /// </summary>
        /// <value>The GOBIERNO identifier.</value>
        public char? GobiernoId { get; set; }

        /**TODO: Checar con sergio los siguientes campos, si es que deben ser considerados en esta entidad*/

        /// <summary>
        /// Gets or sets the CARGO text.
        /// </summary>
        /// <value> The CARGO text.</value>
        public string Cargo { get; set; }

        /// <summary>
        /// Gets or sets the TELEFONO text.
        /// </summary>
        /// <value> The TELEFONO text.</value>
        public string Telefono { get; set; }

        /// <summary>
        /// Gets or sets the CORREO ELECTRONICO text.
        /// </summary>
        /// <value> The CORREO ELECTRONICO text.</value>
        public string CorreoElectronico { get; set; }

        /// <summary>
        /// Gets or sets the DOMICILIO text.
        /// </summary>
        /// <value> The DOMICILIO text.</value>
        public string Domicilio { get; set; }
    }
}
