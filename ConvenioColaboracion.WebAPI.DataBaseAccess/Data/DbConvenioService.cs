//-----------------------------------------------------------------------
// <copyright file="DbConvenioService.cs" company="SFP">
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
    /// The CONVENIO database service implementation.
    /// </summary>
    public class DbConvenioService : IDbConvenioService
    {
        /// <summary>
        /// The data access factory
        /// </summary>
        private readonly IDatabaseHelper databaseHelper;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DbConvenioService"/> class. 
        /// </summary>
        /// <param name="databaseHelper">The database helper.</param>
        public DbConvenioService(IDatabaseHelper databaseHelper)
        {
            //// TODO: Check if we can add the logger here
            this.databaseHelper = databaseHelper;
        }
        #endregion

        /// <summary>
        /// Inserts the CONVENIO data in the database.
        /// </summary>
        /// <param name="request">The requested entity.</param>
        /// <returns>A value indicating whether the data was successful inserted or not.</returns>
        public bool Insert(EConvenio request)
        {
            // The expected result.
            var result = false;

            OracleTransaction transaction = null;

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_CONVENIO_INSERT";

            try
            {
                // Create the database connection from the helper.
                using (var connection = this.databaseHelper.CreateDbConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    connection.Open();

                    transaction = (OracleTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);

                    // Create database command object.
                    using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                    {
                        command.Transaction = transaction;

                        // Clear the parameters.
                        command.Parameters.Clear();

                        // Add the parameters to the list.
                        command.Parameters.Add(this.databaseHelper.CreateParameter("numeroConvenio", OracleDbType.Varchar2, request.NumeroConvenio));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("convenio", OracleDbType.Varchar2, request.Convenio));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("nombreCorto", OracleDbType.Varchar2, request.NombreCorto));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("resumen", OracleDbType.Varchar2, request.Resumen));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaSuscripcion", OracleDbType.Date, request.FechaSuscripcion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaTermino", OracleDbType.Date, request.FechaTermino));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("materiaId", OracleDbType.Int32, request.MateriaId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("submateriaId", OracleDbType.Int32, request.SubmateriaId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("areaVinculanteId", OracleDbType.Int32, request.AreaVinculanteId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("sexenioId", OracleDbType.Int32, request.SexenioId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("año", OracleDbType.Int32, request.Año));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaCreacion", OracleDbType.Date, request.FechaCreacion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("usuarioCreacion", OracleDbType.Varchar2, request.UsuarioCreacion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaUltimaActividad", OracleDbType.Date, request.FechaUltimaActividad));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("avance", OracleDbType.Int32, request.Avance));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("estatus", OracleDbType.Char, request.Estatus));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("rutaDocumento", OracleDbType.Varchar2, request.RutaDocumento));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                        // Execute the stored procedure.
                        command.ExecuteNonQuery();

                        var isInserted = ((IDataParameter)command.Parameters["affectedRows"]).Value == DBNull.Value ||
                                                 ((IDataParameter)command.Parameters["affectedRows"]).Value == null
                                                 ? 0
                                                 : Convert.ToInt32(((IDataParameter)command.Parameters["affectedRows"]).Value.ToString());

                        if (isInserted > 0)
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
        /// Updates the specified CONVENIO in the database.
        /// </summary>
        /// <param name="request">The requested entity.</param>
        /// <returns> A value indicating whether the data was successful updated or not.</returns>
        public bool Update(EConvenio request)
        {
            // The expected result.
            var result = false;

            OracleTransaction transaction = null;

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_CONVENIO_UPDATE";

            try
            {
                // Create the database connection from the helper.
                using (var connection = this.databaseHelper.CreateDbConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    connection.Open();

                    transaction = (OracleTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);

                    // Create database command object.
                    using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                    {
                        command.Transaction = transaction;

                        // Clear the parameters.
                        command.Parameters.Clear();

                        // Add the parameters to the list.
                        command.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, request.ConvenioId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("numeroConvenio", OracleDbType.Varchar2, request.NumeroConvenio));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("convenio", OracleDbType.Varchar2, request.Convenio));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("nombreCorto", OracleDbType.Varchar2, request.NombreCorto));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("resumen", OracleDbType.Varchar2, request.Resumen));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaSuscripcion", OracleDbType.Date, request.FechaSuscripcion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaTermino", OracleDbType.Date, request.FechaTermino));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("materiaId", OracleDbType.Int32, request.MateriaId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("submateriaId", OracleDbType.Int32, request.SubmateriaId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("areaVinculanteId", OracleDbType.Int32, request.AreaVinculanteId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("sexenioId", OracleDbType.Int32, request.SexenioId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("año", OracleDbType.Int32, request.Año));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaActualizacion", OracleDbType.Date, request.FechaActualizacion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("usuarioActualizacion", OracleDbType.Varchar2, request.UsuarioActualizacion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaUltimaActividad", OracleDbType.Date, request.FechaUltimaActividad));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("avance", OracleDbType.Int32, request.Avance));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("estatus", OracleDbType.Char, request.Estatus));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("rutaDocumento", OracleDbType.Varchar2, request.RutaDocumento));
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
        /// Deletes the CONVENIO in the database.
        /// </summary>
        /// <param name="request">The requested entity.</param>
        /// <returns> A value indicating whether the data was successful deleted or not.</returns>
        public bool Delete(EConvenio request)
        {
            // The expected result.
            var result = false;

            OracleTransaction transaction = null;

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_CONVENIO_DELETE";

            try
            {
                // Create the database connection from the helper.
                using (var connection = this.databaseHelper.CreateDbConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    connection.Open();

                    transaction = (OracleTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);

                    // Create database command object.
                    using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                    {
                        command.Transaction = transaction;

                        // Clear the parameters.
                        command.Parameters.Clear();

                        // Add the parameters to the list.
                        command.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, request.ConvenioId));
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
        /// Gets the CONVENIO list from the database. 
        /// </summary>
        /// <returns> The list of CONVENIOS.</returns>
        public IEnumerable<EConvenio> GetAll()
        {
            // The expected model list.
            var convenioList = new List<EConvenio>();

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_CONVENIO_SELECT";

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
                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var convenio = GetConvenioFromReader(reader);

                                        // Add the element to the list.
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

        /// <summary>
        /// Gets the specified CONVENIO from the database. 
        /// </summary>
        /// <param name="convenioId">The CONVENIO identifier.</param>
        /// <returns>Expected CONVENIO model.</returns>
        public EConvenio Get(int convenioId)
        {
            var convenio = new EConvenio();

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_CONVENIO_SELECT_BY_ID";

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
                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("idConvenio", OracleDbType.Int32, convenioId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    if (reader.Read())
                                    {
                                        convenio = GetConvenioFromReader(reader);
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

            return convenio;
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
            convenio.FechaSuscripcion = reader["FECHA_SUSCRIPCION"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_SUSCRIPCION"]);
            convenio.FechaTermino = reader["FECHA_TERMINO"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_TERMINO"]);
            convenio.MateriaId = reader["ID_MATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_MATERIA"]);
            convenio.SubmateriaId = reader["ID_SUBMATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_SUBMATERIA"]);
            convenio.AreaVinculanteId = reader["ID_AREA_VINCULANTE"] is DBNull ? 0 : Convert.ToInt32(reader["ID_AREA_VINCULANTE"]);
            convenio.SexenioId = reader["SEXENIO"] is DBNull ? 0 : Convert.ToInt32(reader["SEXENIO"]);
            convenio.Año = reader["AÑO"] is DBNull ? 0 : Convert.ToInt32(reader["AÑO"]);
            convenio.FechaCreacion = reader["FECHA_CREACION"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_CREACION"]);
            convenio.FechaActualizacion = reader["FECHA_ACTUALIZACION"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_ACTUALIZACION"]);
            convenio.UsuarioCreacion = reader["USUARIO_CREACION"] is DBNull ? string.Empty : Convert.ToString(reader["USUARIO_CREACION"]);
            convenio.UsuarioActualizacion = reader["USUARIO_ACTUALIZACION"] is DBNull ? string.Empty : Convert.ToString(reader["USUARIO_ACTUALIZACION"]);
            convenio.FechaUltimaActividad = reader["FECHA_ULTIMA_ACTIVIDAD"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["FECHA_ULTIMA_ACTIVIDAD"]);
            convenio.Avance = reader["AVANCE"] is DBNull ? 0 : Convert.ToInt32(reader["AVANCE"]);
            convenio.Estatus = reader["ESTATUS"] is DBNull ? char.MinValue : Convert.ToChar(reader["ESTATUS"]);
            convenio.RutaDocumento = reader["RUTA_DOCUMENTO"] is DBNull ? string.Empty : Convert.ToString(reader["RUTA_DOCUMENTO"]);

            return convenio;
        }
        #endregion
    }
}
