//-----------------------------------------------------------------------
// <copyright file="EConvenioParte.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The CONVENIO PARTE request model.
    /// </summary>
    public class EConvenioParte
    {
        /// <summary>
        /// Gets or sets the CONVENIO identifier.
        /// </summary>
        /// <value>The CONVENIO identifier.</value>
        public int ConvenioId { get; set; }

        /// <summary>
        /// Gets or sets the PARTE model.
        /// </summary>
        /// <value>The PARTE model.</value>
        public EParte Parte { get; set; }

        /// <summary>
        /// Gets or sets the PARTE identifier.
        /// </summary>
        /// <value>The PARTE identifier.</value>
        public int ParteId { get; set; }

        /// <summary>
        /// Gets or sets the REPRESENTANTE text.
        /// </summary>
        /// <value> The REPRESENTANTE text.</value>
        [Required(ErrorMessage = "Representante is required.")]
        public string Representante { get; set; }

        /// <summary>
        /// Gets or sets the CARGO text.
        /// </summary>
        /// <value> The CARGO text.</value>
        [Required(ErrorMessage = "Cargo is required.")]
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
