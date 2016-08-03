//-----------------------------------------------------------------------
// <copyright file="EInformePeriodo.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The INFORME PERIODO request model.
    /// </summary>
    public class EInformePeriodo
    {
        /// <summary>
        /// Gets or sets the ADMINISTRACION identifier.
        /// </summary>
        /// <value>The ADMINISTRACION identifier.</value>
        public int AdministracionId { get; set; }

        /// <summary>
        /// Gets or sets the MATERIA identifier.
        /// </summary>
        /// <value>The MATERIA identifier.</value>
        public int MateriaId { get; set; }

        /// <summary>
        /// Gets or sets the AÑO identifier.
        /// </summary>
        /// <value>The AÑO identifier.</value>
        public int AñoId { get; set; }

        /// <summary>
        /// Gets or sets the AGRUPAR ADMINISTRACION identifier.
        /// </summary>
        /// <value>The AGRUPAR ADMINISTRACION identifier.</value>
        public int AgruparAdministracion { get; set; }

        /// <summary>
        /// Gets or sets the AGRUPAR MATERIA identifier.
        /// </summary>
        /// <value>The AGRUPAR MATERIA identifier.</value>
        public int AgruparMateria { get; set; }

        /// <summary>
        /// Gets or sets the AGRUPAR AÑO identifier.
        /// </summary>
        /// <value>The AGRUPAR AÑO identifier.</value>
        public int AgruparAño { get; set; }
    }
}
