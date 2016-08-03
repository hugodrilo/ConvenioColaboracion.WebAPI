//-----------------------------------------------------------------------
// <copyright file="EInformeArea.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Return
{
    /// <summary>
    /// The INFORME AREA return model.
    /// </summary>
    public class EInformeArea
    {
        /// <summary>
        /// Gets or sets the ORDEN identifier.
        /// </summary>
        /// <value>The ORDEN identifier.</value>
        public int Orden { get; set; }

        /// <summary>
        /// Gets or sets the area identifier.
        /// </summary>
        /// <value>The area identifier.</value>
        public int AreaId { get; set; }

        /// <summary>
        /// Gets or sets the SIGLAS text.
        /// </summary>
        /// <value>The SIGLAS text.</value>
        public string Siglas { get; set; }

        /// <summary>
        /// Gets or sets the area text.
        /// </summary>
        /// <value>The area text.</value>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the RESPONSABLE INSTITUCIONAL text.
        /// </summary>
        /// <value>The RESPONSABLE INSTITUCIONAL text.</value>
        public string ResponsableInstitucional { get; set; }

        /// <summary>
        /// Gets or sets the RESPONSABLE OPERATIVO text.
        /// </summary>
        /// <value>The RESPONSABLE OPERATIVO text.</value>
        public string ResponsableOperativo { get; set; }
    }
}
