//-----------------------------------------------------------------------
// <copyright file="EParte.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The PARTE request model.
    /// </summary>
    public class EParte
    {
        /// <summary>
        /// Gets or sets the PARTE identifier.
        /// </summary>
        /// <value>The PARTE identifier.</value>
        public int ParteId { get; set; }

        /// <summary>
        /// Gets or sets the PARTE text.
        /// </summary>
        /// <value> The PARTE text.</value>
        public string Parte { get; set; }

        /// <summary>
        /// Gets or sets the CLAVE text.
        /// </summary>
        /// <value> The CLAVE text.</value>
        public string Clave { get; set; }

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
        public string PaisId { get; set; }

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
    }
}
