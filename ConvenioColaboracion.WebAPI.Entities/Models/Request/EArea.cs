//-----------------------------------------------------------------------
// <copyright file="EArea.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The Area request model.
    /// </summary>
    public class EArea
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
        /// <value> The UR text.</value>
        public string Ur { get; set; }

        /// <summary>
        /// Gets or sets the area text.
        /// </summary>
        /// <value> The area text.</value>
        public string Area { get; set; }
    }
}
