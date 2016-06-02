//-----------------------------------------------------------------------
// <copyright file="DbActividadService.cs" company="SFP">
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
    using Entities.Models.Request;
    using Oracle.ManagedDataAccess.Client;
    using Utilities;

    /// <summary>
    /// The ACTIVIDAD database service implementation.
    /// </summary>
    public class DbActividadService : IDbActividadService
    {
        /// <summary>
        /// The data access factory
        /// </summary>
        private readonly IDatabaseHelper databaseHelper;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DbActividadService"/> class. 
        /// </summary>
        /// <param name="databaseHelper">The database helper.</param>
        public DbActividadService(IDatabaseHelper databaseHelper)
        {
            //// TODO: Check if we can add the logger here
            this.databaseHelper = databaseHelper;
        }
        #endregion

        /// <summary>
        /// Inserts the ACTIVIDAD data in the database.
        /// </summary>
        /// <param name="request">The requested entity.</param>
        /// <returns>A value indicating whether the data was successful inserted or not.</returns>
        public bool Insert(EActividad request)
        {
            // The expected result.
            var result = false;

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_ACTIVIDAD_INSERT";

            try
            {
                // Create the database connection from the helper.
                using (var connection = this.databaseHelper.CreateDbConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    connection.Open();

                    var transaction = (OracleTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);

                    // Create database command object.
                    using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                    {
                        // Convert string dates to datetime format.
                        var fechaActividad = ParseDateTime(request.FechaActividad, "dd/MM/yyyy");

                        // Start the transaction.
                        command.Transaction = transaction;

                        // Clear the parameters.
                        command.Parameters.Clear();

                        // Add the parameters to the list.
                        command.Parameters.Add(this.databaseHelper.CreateParameter("compromisoId", OracleDbType.Int32, request.CompromisoId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaActividad", OracleDbType.Date, fechaActividad == DateTime.MinValue ? (DateTime?)null : fechaActividad));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("porcentajeAvance", OracleDbType.Int32, request.PorcentajeAvance));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("actividadText", OracleDbType.Varchar2, request.Actividad));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("anexoText", OracleDbType.Varchar2, request.Anexo));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("usuarioCreacion", OracleDbType.Varchar2, request.UsuarioCreacion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("actividadId", OracleDbType.Int32, 0, ParameterDirection.Output));

                        // Execute the stored procedure.
                        command.ExecuteNonQuery();

                        // Get the inserted convenio identifier.
                        request.ActividadId = ((IDataParameter)command.Parameters["actividadId"]).Value == DBNull.Value ||
                                                 ((IDataParameter)command.Parameters["actividadId"]).Value == null
                                                 ? 0
                                                 : Convert.ToInt32(((IDataParameter)command.Parameters["actividadId"]).Value.ToString());

                        if (request.ActividadId > 0)
                        {
                            result = true;
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Updates the specified ACTIVIDAD in the database.
        /// </summary>
        /// <param name="request">The requested entity.</param>
        /// <returns> A value indicating whether the data was successful updated or not.</returns>
        public bool Update(EActividad request)
        {
            // The expected result.
            var result = false;

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_ACTIVIDAD_UPDATE";

            try
            {
                // Create the database connection from the helper.
                using (var connection = this.databaseHelper.CreateDbConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    connection.Open();

                    var transaction = (OracleTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);

                    // Create database command object.
                    using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                    {
                        // Convert string dates to datetime format.
                        var fechaActividad = ParseDateTime(request.FechaActividad, "dd/MM/yyyy");

                        command.Transaction = transaction;

                        // Clear the parameters.
                        command.Parameters.Clear();

                        // Add the parameters to the list.
                        command.Parameters.Add(this.databaseHelper.CreateParameter("actividadId", OracleDbType.Int32, request.ActividadId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("compromisoId", OracleDbType.Int32, request.CompromisoId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaActividad", OracleDbType.Date, fechaActividad == DateTime.MinValue ? (DateTime?)null : fechaActividad));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("porcentajeAvance", OracleDbType.Int32, request.PorcentajeAvance));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("actividadText", OracleDbType.Varchar2, request.Actividad));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("anexoText", OracleDbType.Varchar2, request.Anexo));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("usuarioActualizacion", OracleDbType.Varchar2, request.UsuarioActualizacion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                        // Execute the stored procedure.
                        command.ExecuteNonQuery();

                        var isUpdated = ((IDataParameter)command.Parameters["affectedRows"]).Value == DBNull.Value ||
                                                 ((IDataParameter)command.Parameters["affectedRows"]).Value == null
                                                 ? 0
                                                 : Convert.ToInt32(((IDataParameter)command.Parameters["affectedRows"]).Value.ToString());

                        if (isUpdated > 0)
                        {
                            result = true;
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Deletes the ACTIVIDAD in the database.
        /// </summary>
        /// <param name="request">The requested entity.</param>
        /// <returns> A value indicating whether the data was successful deleted or not.</returns>
        public bool Delete(EActividad request)
        {
            // The expected result.
            var result = false;

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_ACTIVIDAD_DELETE";

            try
            {
                // Create the database connection from the helper.
                using (var connection = this.databaseHelper.CreateDbConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    connection.Open();

                    var transaction = (OracleTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);

                    // Create database command object.
                    using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                    {
                        command.Transaction = transaction;

                        // Clear the parameters.
                        command.Parameters.Clear();

                        // Add the parameters to the list.
                        command.Parameters.Add(this.databaseHelper.CreateParameter("actividadId", OracleDbType.Int32, request.ActividadId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                        // Execute the stored procedure.
                        command.ExecuteNonQuery();

                        var isDeleted = ((IDataParameter)command.Parameters["affectedRows"]).Value == DBNull.Value ||
                                                 ((IDataParameter)command.Parameters["affectedRows"]).Value == null
                                                 ? 0
                                                 : Convert.ToInt32(((IDataParameter)command.Parameters["affectedRows"]).Value.ToString());

                        if (isDeleted > 0)
                        {
                            result = true;
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Gets the specified ACTIVIDAD from the database. 
        /// </summary>
        /// <param name="id">The ACTIVIDAD identifier.</param>
        /// <returns>Expected ACTIVIDAD model.</returns>
        public EActividad Get(int id)
        {
            var actividad = new EActividad();

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
                            const string StoredProcedureName = @"USP_ACTIVIDAD_SELECT_BY_ID";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("idActividad", OracleDbType.Int32, id));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    if (reader.Read())
                                    {
                                        actividad = GetActividadFromReader(reader);
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

            return actividad;
        }

        /// <summary>
        /// Gets the specified ACTIVIDAD list from the database. 
        /// </summary>
        /// <param name="compromisoId">The COMPROMISO identifier.</param>
        /// <returns>Expected ACTIVIDAD model list.</returns>
        public IEnumerable<EActividad> GetAllById(int compromisoId)
        {
            var actividadList = new List<EActividad>();

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
                            const string StoredProcedureName = @"USP_ACTIVIDAD_BY_ID_COMPROMISO";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("idCompromiso", OracleDbType.Int32, compromisoId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var actividad = GetActividadFromReader(reader);

                                        actividadList.Add(actividad);
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

            return actividadList;
        }

        #region Auxiliar Methods
        /// <summary>
        /// Parses the string date to the specified date time format.
        /// </summary>
        /// <param name="date">The date to be parsed.</param>
        /// <param name="format">The date time format.</param>
        /// <returns>Expected date in the specified format.</returns>
        private static DateTime ParseDateTime(string date, string format)
        {
            return !string.IsNullOrEmpty(date)
                ? DateTime.ParseExact(date, format, System.Globalization.CultureInfo.InvariantCulture)
                : DateTime.MinValue;
        }

        /// <summary>
        /// Gets the ACTIVIDAD from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected ACTIVIDAD model.</returns>
        private static EActividad GetActividadFromReader(IDataReader reader)
        {
            var actividad = new EActividad();

            actividad.ActividadId = reader["ID_ACTIVIDAD"] is DBNull ? 0 : Convert.ToInt32(reader["ID_ACTIVIDAD"]);
            actividad.CompromisoId = reader["ID_COMPROMISO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_COMPROMISO"]);
            actividad.FechaActividad = reader["FECHA_ACTIVIDAD"] is DBNull ? string.Empty : Convert.ToString(reader["FECHA_ACTIVIDAD"]);
            actividad.PorcentajeAvance = reader["PORCENTAJE_AVANCE"] is DBNull ? 0 : Convert.ToInt32(reader["PORCENTAJE_AVANCE"]);
            actividad.Actividad = reader["ACTIVIDAD"] is DBNull ? string.Empty : Convert.ToString(reader["ACTIVIDAD"]);
            actividad.Anexo = reader["ANEXO"] is DBNull ? string.Empty : Convert.ToString(reader["ANEXO"]); ////(RutaDocumento)
            actividad.NombreDocumento = string.IsNullOrEmpty(actividad.Anexo) ? string.Empty : actividad.Anexo.Substring(actividad.Anexo.LastIndexOf('\\') + 1);
            actividad.FechaCreacion = reader["FECHA_CREACION"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_CREACION"]);
            actividad.FechaActualizacion = reader["FECHA_ACTUALIZACION"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_ACTUALIZACION"]);
            actividad.UsuarioCreacion = reader["USUARIO_CREACION"] is DBNull ? string.Empty : Convert.ToString(reader["USUARIO_CREACION"]);
            actividad.UsuarioActualizacion = reader["USUARIO_ACTUALIZACION"] is DBNull ? string.Empty : Convert.ToString(reader["USUARIO_ACTUALIZACION"]);

            return actividad;
        }
        #endregion
    }
}
