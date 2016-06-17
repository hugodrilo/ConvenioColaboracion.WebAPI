//-----------------------------------------------------------------------
// <copyright file="EEstatus.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The ESTATUS request model.
    /// </summary>
    public class EEstatus
    {
        /// <summary>
        /// Gets or sets the ESTATUS identifier.
        /// </summary>
        /// <value>The ESTATUS identifier.</value>
        public int Estatus { get; set; }

        /// <summary>
        /// Gets or sets the ESTATUS text.
        /// </summary>
        /// <value> The ESTATUS text.</value>
        public string Descripcion { get; set; }
    }
}
