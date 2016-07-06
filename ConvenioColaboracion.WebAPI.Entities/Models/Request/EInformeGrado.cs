//-----------------------------------------------------------------------
// <copyright file="EInformeGrado.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The INFORME MATERIA request model.
    /// </summary>
    public class EInformeGrado
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
        /// Gets or sets the SUBMATERIA identifier.
        /// </summary>
        /// <value>The SUBMATERIA identifier.</value>
        public int SubMateriaId { get; set; }

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
        /// Gets or sets the AGRUPAR SUBMATERIA identifier.
        /// </summary>
        /// <value>The AGRUPAR SUBMATERIA identifier.</value>
        public int AgruparSubMateria { get; set; }
    }
}
