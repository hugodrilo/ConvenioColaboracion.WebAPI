//-----------------------------------------------------------------------
// <copyright file="EInformePeriodo.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Return
{
    /// <summary>
    /// The INFORME PERIODO return model.
    /// </summary>
    public class EInformePeriodo
    {
        /// <summary>
        /// Gets or sets the ADMINISTRACION identifier.
        /// </summary>
        /// <value>The ADMINISTRACION identifier.</value>
        public int AdministracionId { get; set; }

        /// <summary>
        /// Gets or sets the ADMINISTRACION text.
        /// </summary>
        /// <value>The ADMINISTRACION text.</value>
        public string Administracion { get; set; }

        /// <summary>
        /// Gets or sets the AÑO.
        /// </summary>
        /// <value>The AÑO number.</value>
        public int? Año { get; set; }

        /// <summary>
        /// Gets or sets the MATERIA identifier.
        /// </summary>
        /// <value>The MATERIA identifier.</value>
        public int MateriaId { get; set; }

        /// <summary>
        /// Gets or sets the MATERIA text.
        /// </summary>
        /// <value>The MATERIA text.</value>
        public string Materia { get; set; }

        /// <summary>
        /// Gets or sets the NUMERO CONVENIOS.
        /// </summary>
        /// <value>The NUMERO CONVENIO number.</value>
        public int? NumeroConvenio { get; set; }
    }
}
