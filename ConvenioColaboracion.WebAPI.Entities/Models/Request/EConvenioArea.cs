//-----------------------------------------------------------------------
// <copyright file="EConvenioArea.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The CONVENIO AREA request model.
    /// </summary>
    public class EConvenioArea
    {
        /// <summary>
        /// Gets or sets the CONVENIO identifier.
        /// </summary>
        /// <value>The CONVENIO identifier.</value>
        public int ConvenioId { get; set; }

        /// <summary>
        /// Gets or sets the AREA model.
        /// </summary>
        /// <value>The AREA model.</value>
        public EArea Area { get; set; }

        /// <summary>
        /// Gets or sets the AREA identifier.
        /// </summary>
        /// <value>The AREA identifier.</value>
        public int AreaId { get; set; }

        /// <summary>
        /// Gets or sets the TIPOAREA model.
        /// </summary>
        /// <value>The TIPOAREA model.</value>
        public ETipoArea TipoArea { get; set; }

        /// <summary>
        /// Gets or sets the TIPOAREA identifier.
        /// </summary>
        /// <value>The TIPOAREA identifier.</value>
        public int TipoAreaId { get; set; }
    }
}
