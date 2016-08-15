//-----------------------------------------------------------------------
// <copyright file="EAlerta.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Return
{
    /// <summary>
    /// The ALERTA return model.
    /// </summary>
    public class EAlerta
    {
        /// <summary>
        /// Gets or sets the ESTATUS identifier.
        /// </summary>
        /// <value>The ESTATUS identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the CONVENIO identifier.
        /// </summary>
        /// <value>The CONVENIO identifier.</value>
        public int ConvenioId { get; set; }

        /// <summary>
        /// Gets or sets the AÑO number.
        /// </summary>
        /// <value>The AÑO number.</value>
        public int Año { get; set; }

        /// <summary>
        /// Gets or sets the NUMERO CONVENIO text.
        /// </summary>
        /// <value>The NUMERO CONVENIO text.</value>
        public string NumeroConvenio { get; set; }

        /// <summary>
        /// Gets or sets the CONVENIO text.
        /// </summary>
        /// <value>The CONVENIO text.</value>
        public string Convenio { get; set; }

        /// <summary>
        /// Gets or sets the NOMBRE CORTO text.
        /// </summary>
        /// <value>The NOMBRE CORTO text.</value>
        public string NombreCorto { get; set; }

        /// <summary>
        /// Gets or sets the RESUMEN text.
        /// </summary>
        /// <value>The RESUMEN text.</value>
        public string Resumen { get; set; }

        /// <summary>
        /// Gets or sets the FECHA SUSCRIPCION text.
        /// </summary>
        /// <value>The FECHA SUSCRIPCION text.</value>
        public string FechaSuscripcion { get; set; }

        /// <summary>
        /// Gets or sets the FECHA TERMINO text.
        /// </summary>
        /// <value>The FECHA TERMINO text.</value>
        public string FechaTermino { get; set; }

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
        /// Gets or sets the SEXENIO identifier.
        /// </summary>
        /// <value>The SEXENIO identifier.</value>
        public int SexenioId { get; set; }

        /// <summary>
        /// Gets or sets the AVANCE number.
        /// </summary>
        /// <value>The AVANCE number.</value>
        public int Avance { get; set; }

        /// <summary>
        /// Gets or sets the ESTATUS number.
        /// </summary>
        /// <value>The ESTATUS number.</value>
        public int Estatus { get; set; }

        /// <summary>
        /// Gets or sets the RUTA DOCUMENTO on the server.
        /// </summary>
        /// <value> The RUTA DOCUMENTO on the server.</value>
        public string RutaDocumento { get; set; }

        /// <summary>
        /// Gets or sets the COMENTARIOS.
        /// </summary>
        /// <value> The COMENTARIOS.</value>
        public string Comentarios { get; set; }

        /// <summary>
        /// Gets or sets the logo path on the server.
        /// </summary>
        /// <value> The logo path on the server.</value>
        public string Logo { get; set; }

        /// <summary>
        /// Gets or sets the ULTIMO REPORTE text.
        /// </summary>
        /// <value> The ULTIMO REPORTE text.</value>
        public string UltimoReporte { get; set; }

        /// <summary>
        /// Gets or sets the FECHA COMPROMISO text.
        /// </summary>
        /// <value> The FECHA COMPROMISO text.</value>
        public string FechaCompromiso { get; set; }

        /// <summary>
        /// Gets or sets the AREA VINCULANTE text.
        /// </summary>
        /// <value> The AREA VINCULANTE text.</value>
        public string AreaVinculante { get; set; }
    }
}
