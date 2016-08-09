﻿//-----------------------------------------------------------------------
// <copyright file="EEstadistica.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The ESTADISTICA request model.
    /// </summary>
    public class EEstadistica
    {
        /// <summary>
        /// Gets or sets the area identifier.
        /// </summary>
        /// <value>The area identifier.</value>
        public int AreaId { get; set; }

        /// <summary>
        /// Gets or sets the RAMO identifier.
        /// </summary>
        /// <value>The RAMO identifier.</value>
        public int? Ramo { get; set; }

        /// <summary>
        /// Gets or sets the UR text.
        /// </summary>
        /// <value>The UR text.</value>
        public string Ur { get; set; }

        /// <summary>
        /// Gets or sets the area text.
        /// </summary>
        /// <value>The area text.</value>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the selected flag is true or false.
        /// </summary>
        /// <value>The selected flag.</value>
        public bool Selected { get; set; }
    }
}