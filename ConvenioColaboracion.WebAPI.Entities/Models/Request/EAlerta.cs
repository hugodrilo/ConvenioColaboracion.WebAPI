//-----------------------------------------------------------------------
// <copyright file="EAlerta.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The ALERTA request model.
    /// </summary>
    public class EAlerta
    {
        /// <summary>
        /// Gets or sets the PAGINA number.
        /// </summary>
        /// <value>The PAGINA number.</value>
        public int Pagina { get; set; }

        /// <summary>
        /// Gets or sets the REGISTRO number.
        /// </summary>
        /// <value>The REGISTRO number.</value>
        public int Registro { get; set; }

        /// <summary>
        /// Gets or sets the MESES number.
        /// </summary>
        /// <value>The MESES number.</value>
        public int Meses { get; set; }
    }
}
