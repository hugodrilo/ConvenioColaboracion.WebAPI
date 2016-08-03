//-----------------------------------------------------------------------
// <copyright file="EFiltrosBusqueda.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    using System.Collections.Generic;

    /// <summary>
    /// The FILTRO BUSQUEDA request model.
    /// </summary>
    public class EFiltrosBusqueda
    {
        /// <summary>
        /// Gets or sets the PERIODOS list.
        /// </summary>
        /// <value>The PERIODOS list.</value>
        public IEnumerable<int> Periodos { get; set; }

        /// <summary>
        /// Gets or sets the MATERIAS list.
        /// </summary>
        /// <value>The MATERIAS list.</value>
        public IEnumerable<int> Materias { get; set; }

        /// <summary>
        /// Gets or sets the AVANCES list.
        /// </summary>
        /// <value>The AVANCES list.</value>
        public IEnumerable<string> Avances { get; set; }

        /// <summary>
        /// Gets or sets the PARTES list.
        /// </summary>
        /// <value>The PARTES list.</value>
        public IEnumerable<int> Partes { get; set; }

        /// <summary>
        /// Gets or sets the areas list.
        /// </summary>
        /// <value>The areas list.</value>
        public IEnumerable<int> Areas { get; set; }

        /// <summary>
        /// Gets or sets the RI list.
        /// </summary>
        /// <value>The RI list.</value>
        public IEnumerable<int> RI { get; set; }

        /// <summary>
        /// Gets or sets the RO list.
        /// </summary>
        /// <value>The RO list.</value>
        public IEnumerable<int> RO { get; set; }
    }
}
