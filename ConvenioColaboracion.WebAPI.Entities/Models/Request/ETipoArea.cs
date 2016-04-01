//-----------------------------------------------------------------------
// <copyright file="ETipoArea.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The TIPOAREA request model.
    /// </summary>
    public class ETipoArea
    {
        /// <summary>
        /// Gets or sets the TIPOAREA identifier.
        /// </summary>
        /// <value>The TIPOAREA identifier.</value>
        public int TipoAreaId { get; set; }

        /// <summary>
        /// Gets or sets the TIPOAREA text.
        /// </summary>
        /// <value> The TIPOAREA text.</value>
        public string TipoArea { get; set; }
    }
}
