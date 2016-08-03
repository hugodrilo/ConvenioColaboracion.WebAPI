//-----------------------------------------------------------------------
// <copyright file="ESexenio.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    using System;

    /// <summary>
    /// The SEXENIO request model.
    /// </summary>
    public class ESexenio
    {
        /// <summary>
        /// Gets or sets the SEXENIO identifier.
        /// </summary>
        /// <value>The SEXENIO identifier.</value>
        public int SexenioId { get; set; }

        /// <summary>
        /// Gets or sets the SEXENIO text.
        /// </summary>
        /// <value> The SEXENIO text.</value>
        public string Sexenio { get; set; }

        /// <summary>
        /// Gets or sets the SEXENIO text.
        /// </summary>
        /// <value> The SEXENIO text.</value>
        public DateTime? Desde { get; set; }

        /// <summary>
        /// Gets or sets the SEXENIO text.
        /// </summary>
        /// <value> The SEXENIO text.</value>
        public DateTime? Hasta { get; set; }
    }
}
