//-----------------------------------------------------------------------
// <copyright file="EInformeGrado.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Return
{
    /// <summary>
    /// The INFORME GRADO return model.
    /// </summary>
    public class EInformeGrado
    {
        /// <summary>
        /// Gets or sets the ESTATUS identifier.
        /// </summary>
        /// <value>The ESTATUS identifier.</value>
        public int EstatusId { get; set; }

        /// <summary>
        /// Gets or sets the ESTATUS text.
        /// </summary>
        /// <value>The ESTATUS text.</value>
        public string Estatus { get; set; }

        /// <summary>
        /// Gets or sets the NUMERO CONVENIOS.
        /// </summary>
        /// <value>The NUMERO CONVENIO number.</value>
        public int? NumeroConvenio { get; set; }
    }
}
