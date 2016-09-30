//-----------------------------------------------------------------------
// <copyright file="DbConsultaService.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using Entities.Models.Request;
    using Oracle.ManagedDataAccess.Client;
    using Utilities;

    /// <summary>
    /// The CONVENIO database service implementation.
    /// </summary>
    public class DbConsultaService : IDbConsultaService
    {
        /// <summary>
        /// The data access factory
        /// </summary>
        private readonly IDatabaseHelper databaseHelper;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DbConsultaService"/> class. 
        /// </summary>
        /// <param name="databaseHelper">The database helper.</param>
        public DbConsultaService(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        #endregion

        /// <summary>
        /// Gets all the CONVENIOS from the database that matches the specified text. 
        /// </summary>
        /// <param name="searchText">The text to search.</param>
        /// <param name="usuarioId">The user identifier.</param>
        /// <param name="operacionId">The operation identifier.</param>
        /// <returns>Expected list of CONVENIOS.</returns>
        public IEnumerable<EConvenio> Search(string searchText, int usuarioId = 0, int operacionId = 1)
        {
            var convenioList = new List<EConvenio>();

            try
            {
                // Create the database connection from the helper.
                using (var connection = this.databaseHelper.CreateDbConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    connection.Open();

                    // Create database command object for delete and insert.
                    using (var transaction = connection.BeginTransaction())
                    {
                        if (transaction != null)
                        {
                            // The SQL stored procedure name.
                            const string StoredProcedureName = @"USP_BUSQUEDA_CONVENIO_SELECT";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("searchText", OracleDbType.Varchar2, searchText));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var convenio = GetConvenioFromReader(reader);

                                        convenioList.Add(convenio);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return convenioList;
        }

        #region Auxiliar Methods
        /// <summary>
        /// Gets the CONVENIO from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected CONVENIO model</returns>
        private static EConvenio GetConvenioFromReader(IDataReader reader)
        {
            var convenio = new EConvenio();

            convenio.ConvenioId = reader["ID_CONVENIO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_CONVENIO"]);
            convenio.NumeroConvenio = reader["NUMERO_CONVENIO"] is DBNull ? string.Empty : Convert.ToString(reader["NUMERO_CONVENIO"]);
            convenio.Convenio = reader["CONVENIO"] is DBNull ? string.Empty : Convert.ToString(reader["CONVENIO"]);
            convenio.NombreCorto = reader["NOMBRE_CORTO"] is DBNull ? string.Empty : Convert.ToString(reader["NOMBRE_CORTO"]);
            convenio.Resumen = reader["RESUMEN"] is DBNull ? string.Empty : Convert.ToString(reader["RESUMEN"]);
            convenio.FechaSuscripcion = reader["FECHA_SUSCRIPCION"] is DBNull ? string.Empty : Convert.ToString(reader["FECHA_SUSCRIPCION"]);
            convenio.FechaTermino = reader["FECHA_TERMINO"] is DBNull ? string.Empty : Convert.ToString(reader["FECHA_TERMINO"]);
            convenio.Materia = new EMateria();
            convenio.SubMateria = new EMateria();
            convenio.Materia.MateriaId = reader["ID_MATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_MATERIA"]);
            convenio.SubMateria.MateriaId = reader["ID_SUBMATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_SUBMATERIA"]);
            convenio.SexenioId = reader["SEXENIO"] is DBNull ? 0 : Convert.ToInt32(reader["SEXENIO"]);
            convenio.Año = reader["AÑO"] is DBNull ? 0 : Convert.ToInt32(reader["AÑO"]);
            convenio.FechaCreacion = reader["FECHA_CREACION"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_CREACION"]);
            convenio.FechaActualizacion = reader["FECHA_ACTUALIZACION"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_ACTUALIZACION"]);
            convenio.UsuarioCreacion = reader["USUARIO_CREACION"] is DBNull ? string.Empty : Convert.ToString(reader["USUARIO_CREACION"]);
            convenio.UsuarioActualizacion = reader["USUARIO_ACTUALIZACION"] is DBNull ? string.Empty : Convert.ToString(reader["USUARIO_ACTUALIZACION"]);
            convenio.FechaUltimaActividad = reader["FECHA_ULTIMA_ACTIVIDAD"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_ULTIMA_ACTIVIDAD"]);
            convenio.Avance = reader["AVANCE"] is DBNull ? 0 : Convert.ToInt32(reader["AVANCE"]);
            convenio.Estatus = new EEstatus();
            convenio.Estatus.Estatus = reader["ESTATUS"] is DBNull ? char.MinValue : Convert.ToChar(reader["ESTATUS"]);
            convenio.RutaDocumento = reader["RUTA_DOCUMENTO"] is DBNull ? string.Empty : Convert.ToString(reader["RUTA_DOCUMENTO"]);
            convenio.NombreDocumento = string.IsNullOrEmpty(convenio.RutaDocumento) ? string.Empty : convenio.RutaDocumento.Substring(convenio.RutaDocumento.LastIndexOf('\\') + 1);
            convenio.Comentarios = reader["COMENTARIOS"] is DBNull ? string.Empty : Convert.ToString(reader["COMENTARIOS"]);

            return convenio;
        }
        #endregion
    }
}
