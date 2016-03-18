//-----------------------------------------------------------------------
// <copyright file="EMateria.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The MATERIA request model.
    /// </summary>
    public class EMateria
    {
        /// <summary>
        /// Gets or sets the MATERIA identifier.
        /// </summary>
        /// <value>The MATERIA identifier.</value>
        [Required(ErrorMessage = "materiaId is required.")]
        [Display(Name = "materiaId")]
        public int MateriaId { get; set; }

        /// <summary>
        /// Gets or sets the GRUPO MATERIA identifier.
        /// </summary>
        /// <value>The GRUPO MATERIA identifier.</value>
        [Required(ErrorMessage = "grupoMateriaId is required.")]
        [Display(Name = "grupoMateriaId")]
        public int GrupoMateriaId { get; set; }

        /// <summary>
        /// Gets or sets the MATERIA text.
        /// </summary>
        /// <value> The MATERIA text.</value>
        public string Materia { get; set; }
    }
}
