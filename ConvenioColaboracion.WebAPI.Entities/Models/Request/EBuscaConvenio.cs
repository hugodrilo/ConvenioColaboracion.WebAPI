//-----------------------------------------------------------------------
// <copyright file="EBuscaConvenio.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The BUSCA CONVENIO request model.
    /// </summary>
    public class EBuscaConvenio
    {
        /// <summary>
        /// Gets or sets the PAGINA identifier.
        /// </summary>
        /// <value>The PAGINA identifier.</value>
        public int Pagina { get; set; }

        /// <summary>
        /// Gets or sets the REGISTROS identifier.
        /// </summary>
        /// <value>The REGISTROS identifier.</value>
        public int Registros { get; set; }

        /// <summary>
        /// Gets or sets the key words.
        /// </summary>
        /// <value> The key words.</value>
        public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the FILTROS model.
        /// </summary>
        /// <value> The FILTROS model.</value>
        public EFiltrosBusqueda Filtros { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int? UsuarioId { get; set; }

        /// <summary>
        /// Gets or sets the operation identifier.
        /// </summary>
        /// <value>The operation identifier.</value>
        public int? OperacionId { get; set; }
    }
}
