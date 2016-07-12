//-----------------------------------------------------------------------
// <copyright file="EBuscaConvenio.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Return
{
    /// <summary>
    /// The BUSCA CONVENIO return model.
    /// </summary>
    public class EBuscaConvenio
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the CONVENIO identifier.
        /// </summary>
        /// <value>The CONVENIO identifier.</value>
        public int ConvenioId { get; set; }

        /// <summary>
        /// Gets or sets the NUMERO CONVENIO.
        /// </summary>
        /// <value> The NUMERO CONVENIO.</value>
        public string NumeroConvenio { get; set; }

        /// <summary>
        /// Gets or sets the CONVENIO text.
        /// </summary>
        /// <value> The CONVENIO text.</value>
        public string Convenio { get; set; }

        /// <summary>
        /// Gets or sets the NOMBRE CORTO.
        /// </summary>
        /// <value> The NOMBRE CORTO.</value>
        public string NombreCorto { get; set; }

        /// <summary>
        /// Gets or sets the RESUMEN.
        /// </summary>
        /// <value> The RESUMEN.</value>
        public string Resumen { get; set; }

        /// <summary>
        /// Gets or sets the FECHA SUSCRIPCION.
        /// </summary>
        /// <value> The FECHA SUSCRIPCION.</value>
        public string FechaSuscripcion { get; set; }

        /// <summary>
        /// Gets or sets the FECHA TERMINO.
        /// </summary>
        /// <value> The FECHA TERMINO.</value>
        public string FechaTermino { get; set; }

        /// <summary>
        /// Gets or sets the MATERIA.
        /// </summary>
        /// <value>The MATERIA model.</value>
        public string Materia { get; set; }

        /// <summary>
        /// Gets or sets the SEXENIO identifier.
        /// </summary>
        /// <value>The SEXENIO identifier.</value>
        public int? SexenioId { get; set; }

        /// <summary>
        /// Gets or sets the AVANCE.
        /// </summary>
        /// <value> The AVANCE.</value>
        public int? Avance { get; set; }

        /// <summary>
        /// Gets or sets the ESTATUS.
        /// </summary>
        /// <value> The ESTATUS.</value>
        public int Estatus { get; set; }

        /// <summary>
        /// Gets or sets the RUTA DOCUMENTO on the server.
        /// </summary>
        /// <value> The RUTA DOCUMENTO on the server.</value>
        public string RutaDocumento { get; set; }

        /// <summary>
        /// Gets or sets the COMENTARIOS.
        /// </summary>
        /// <value>The COMENTARIOS text.</value>
        public string Comentarios { get; set; }

        /// <summary>
        /// Gets or sets the LOGO.
        /// </summary>
        /// <value>The LOGO text.</value>
        public string Logo { get; set; }
    }
}
