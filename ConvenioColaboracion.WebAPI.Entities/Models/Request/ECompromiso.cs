//-----------------------------------------------------------------------
// <copyright file="ECompromiso.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The COMPROMISO request model.
    /// </summary>
    public class ECompromiso
    {
        /// <summary>
        /// Gets or sets the COMPROMISO identifier.
        /// </summary>
        /// <value>The COMPROMISO identifier.</value>
        public int CompromisoId { get; set; }

        /// <summary>
        /// Gets or sets the CONVENIO identifier.
        /// </summary>
        /// <value>The CONVENIO identifier.</value>
        public int? ConvenioId { get; set; }

        /// <summary>
        /// Gets or sets the NUMERO COMPROMISO.
        /// </summary>
        /// <value>The NUMERO COMPROMISO .</value>
        public int NumeroCompromiso { get; set; }

        /// <summary>
        /// Gets or sets the PARTE model.
        /// </summary>
        /// <value>The PARTE model.</value>
        public EParte Parte { get; set; }

        /// <summary>
        /// Gets or sets the COMPROMISO text.
        /// </summary>
        /// <value> The COMPROMISO text.</value>
        [Required(ErrorMessage = "Compromiso is required.")]
        public string Compromiso { get; set; }

        /// <summary>
        /// Gets or sets the FECHA COMPROMISO date.
        /// </summary>
        /// <value> The FECHA COMPROMISO date.</value>
        public string FechaCompromiso { get; set; }

        /// <summary>
        /// Gets or sets the AVANCE overall percentage.
        /// </summary>
        /// <value>The AVANCE overall percentage.</value>
        public int? Avance { get; set; }

        /// <summary>
        /// Gets or sets the PONDERACION percentage.
        /// </summary>
        /// <value>The PONDERACION percentage.</value>
        public int? Ponderacion { get; set; }

        /// <summary>
        /// Gets or sets the INSTITUCION identifier.
        /// </summary>
        /// <value> The INSTITUCION identifier.</value>
        public int? InstitucionId { get; set; }

        /// <summary>
        /// Gets or sets the COMPROMISO AREA.
        /// </summary>
        /// <value> The COMPROMISO AREA list.</value>
        public IEnumerable<EArea> Areas { get; set; }
    }
}
