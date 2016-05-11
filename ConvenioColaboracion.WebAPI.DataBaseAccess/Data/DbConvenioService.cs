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
    using System.Linq;
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
                        // Convert string dates to datetime format.
                        var fechaSuscripcion = !string.IsNullOrEmpty(request.FechaSuscripcion) ?
                            DateTime.ParseExact(request.FechaSuscripcion, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) :
                            DateTime.MinValue;

                        var fechaTermino = !string.IsNullOrEmpty(request.FechaTermino) ?
                            DateTime.ParseExact(request.FechaTermino, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) :
                            DateTime.MinValue;

                        // Start the transaction.
                        command.Transaction = transaction;

                        // Clear the parameters.
                        command.Parameters.Clear();

                        // Add the parameters to the list.
                        command.Parameters.Add(this.databaseHelper.CreateParameter("numeroConvenio", OracleDbType.Varchar2, request.NumeroConvenio));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("convenio", OracleDbType.Varchar2, request.Convenio));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("nombreCorto", OracleDbType.Varchar2, request.NombreCorto));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("resumen", OracleDbType.Varchar2, request.Resumen));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaSuscripcion", OracleDbType.Date, fechaSuscripcion == DateTime.MinValue ? (DateTime?)null : fechaSuscripcion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaTermino", OracleDbType.Date, fechaTermino == DateTime.MinValue ? (DateTime?)null : fechaTermino));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("materiaId", OracleDbType.Int32, request.Materia.MateriaId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("subMateriaId", OracleDbType.Int32, request.SubMateria.MateriaId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("sexenioId", OracleDbType.Int32, request.SexenioId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("año", OracleDbType.Int32, request.Año));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaCreacion", OracleDbType.Date, request.FechaCreacion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("usuarioCreacion", OracleDbType.Varchar2, request.UsuarioCreacion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaUltimaActividad", OracleDbType.Date, request.FechaUltimaActividad));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("avance", OracleDbType.Int32, request.Avance));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("estatus", OracleDbType.Char, request.Estatus));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("rutaDocumento", OracleDbType.Varchar2, request.RutaDocumento));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("comentarios", OracleDbType.Varchar2, request.Comentarios));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, 0, ParameterDirection.Output));

                        // Execute the stored procedure.
                        command.ExecuteNonQuery();

                        // Get the inserted convenio identifier.
                        request.ConvenioId = ((IDataParameter)command.Parameters["convenioId"]).Value == DBNull.Value ||
                                                 ((IDataParameter)command.Parameters["convenioId"]).Value == null
                                                 ? 0
                                                 : Convert.ToInt32(((IDataParameter)command.Parameters["convenioId"]).Value.ToString());

                        if (request.ConvenioId > 0)
                        {
                            // Insert convenio Area
                            this.InsertConvenioArea(request, connection);

                            // Insert convenio parte
                            this.InsertConvenioParte(request, connection);

                            // Insert convenio compromiso
                            this.InsertConvenioCompromiso(request, connection);

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
                        // Convert string dates to datetime format.
                        var fechaSuscripcion = !string.IsNullOrEmpty(request.FechaSuscripcion) ?
                            DateTime.ParseExact(request.FechaSuscripcion, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) :
                            DateTime.MinValue;

                        var fechaTermino = !string.IsNullOrEmpty(request.FechaTermino) ?
                            DateTime.ParseExact(request.FechaTermino, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) :
                            DateTime.MinValue;

                        command.Transaction = transaction;

                        // Clear the parameters.
                        command.Parameters.Clear();

                        // Add the parameters to the list.
                        command.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, request.ConvenioId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("numeroConvenio", OracleDbType.Varchar2, request.NumeroConvenio));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("convenioText", OracleDbType.Varchar2, request.Convenio));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("resumenText", OracleDbType.Varchar2, request.Resumen));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaSuscripcion", OracleDbType.Date, fechaSuscripcion == DateTime.MinValue ? (DateTime?)null : fechaSuscripcion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("fechaTermino", OracleDbType.Date, fechaTermino == DateTime.MinValue ? (DateTime?)null : fechaTermino));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("materiaId", OracleDbType.Int32, request.Materia.MateriaId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("subMateriaId", OracleDbType.Int32, request.SubMateria.MateriaId));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("usuarioActualizacion", OracleDbType.Varchar2, request.UsuarioActualizacion));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("rutaDocumento", OracleDbType.Varchar2, request.RutaDocumento));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("comentariosText", OracleDbType.Varchar2, request.Comentarios));
                        command.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                        // Execute the stored procedure.
                        command.ExecuteNonQuery();

                        var isUpdated = ((IDataParameter)command.Parameters["affectedRows"]).Value == DBNull.Value ||
                                                 ((IDataParameter)command.Parameters["affectedRows"]).Value == null
                                                 ? 0
                                                 : Convert.ToInt32(((IDataParameter)command.Parameters["affectedRows"]).Value.ToString());

                        if (isUpdated > 0)
                        {
                            // Delete
                            this.DeleteConvenioParte(request, connection);
                            this.DeleteConvenioArea(request, connection);
                            this.DeleteConvenioCompromiso(request, connection);

                            // Insert
                            this.InsertConvenioArea(request, connection);
                            this.InsertConvenioParte(request, connection);
                            this.InsertConvenioCompromiso(request, connection);

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
                            const string StoredProcedureName = @"USP_CONVENIO_SELECT_BY_ID";

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

                            // Get the convenio areas
                            convenio.Areas = this.GetsConvenioArea(convenioId, connection);

                            // Get the convenio partes
                            convenio.Partes = this.GetsConvenioParte(convenioId, connection);

                            // Get the convenio compromisos
                            convenio.Compromisos = this.GetsConvenioCompromiso(convenioId, connection);

                            // Create the identifiers string 
                            var csvIdCompromiso = string.Join(",", convenio.Compromisos.Select(m => m.CompromisoId));

                            //// USP_PARTES_COMPROMISO
                            convenio.PartesCompromiso = this.GetsPartesCompromiso(csvIdCompromiso, connection);
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

        #region Convenio  Catalogos form
        /// <summary>
        /// Gets the MATERIA list from the database. 
        /// </summary>
        /// <returns> The list of MATERIAS.</returns>
        public IEnumerable<EMateria> GetMateria()
        {
            // The expected model list.
            var materiaList = new List<EMateria>();

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_MATERIA_SELECT";

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
                                        var materia = new EMateria();

                                        materia.MateriaId = reader["ID_MATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_MATERIA"]);
                                        materia.GrupoMateriaId = reader["ID_GRUPO_MATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_GRUPO_MATERIA"]);
                                        materia.Materia = reader["MATERIA"] is DBNull ? string.Empty : Convert.ToString(reader["MATERIA"]);

                                        // Add the element to the list.
                                        materiaList.Add(materia);
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

            return materiaList;
        }

        /// <summary>
        /// Gets all the SUBMATERIAS from the database. 
        /// </summary>
        /// <param name="materiaId">The MATERIA identifier.</param>
        /// <returns>Expected list of SUBMATERIAS.</returns>
        public IEnumerable<EMateria> GetSubMateria(int materiaId)
        {
            // The expected model list.
            var materiaList = new List<EMateria>();

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_SUBMATERIA_SELECT";

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
                                command.Parameters.Add(this.databaseHelper.CreateParameter("materiaId", OracleDbType.Int32, materiaId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var materia = new EMateria();

                                        materia.MateriaId = reader["ID_MATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_MATERIA"]);
                                        materia.GrupoMateriaId = reader["ID_GRUPO_MATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_GRUPO_MATERIA"]);
                                        materia.Materia = reader["MATERIA"] is DBNull ? string.Empty : Convert.ToString(reader["MATERIA"]);

                                        // Add the element to the list.
                                        materiaList.Add(materia);
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

            return materiaList;
        }

        /// <summary>
        /// Gets the area list from the database. 
        /// </summary>
        /// <returns> The list of areas.</returns>
        public IEnumerable<EArea> GetArea()
        {
            // The expected model list.
            var areaList = new List<EArea>();

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_AREA_SELECT";

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
                                        var area = new EArea();

                                        area.AreaId = reader["ID_AREA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_AREA"]);
                                        area.Ramo = reader["RAMO"] is DBNull ? 0 : Convert.ToInt32(reader["RAMO"]);
                                        area.Ur = reader["UR"] is DBNull ? string.Empty : Convert.ToString(reader["UR"]);
                                        area.Area = reader["AREA"] is DBNull ? string.Empty : Convert.ToString(reader["AREA"]);
                                        area.Selected = !(reader["SELECTED"] is DBNull) && Convert.ToBoolean(reader["SELECTED"]);

                                        // Add the element to the list.
                                        areaList.Add(area);
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

            return areaList;
        }

        /// <summary>
        /// Gets the PARTE list from the database. 
        /// </summary>
        /// <returns> The list of PARTES.</returns>
        public IEnumerable<EParte> GetParte()
        {
            // The expected model list.
            var parteList = new List<EParte>();

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_PARTE_SELECT";

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
                                        var parte = new EParte();

                                        parte.ParteId = reader["ID_PARTE"] is DBNull ? 0 : Convert.ToInt32(reader["ID_PARTE"]);
                                        parte.Parte = reader["PARTE"] is DBNull ? string.Empty : Convert.ToString(reader["PARTE"]);
                                        parte.Clave = reader["CLAVE"] is DBNull ? string.Empty : Convert.ToString(reader["CLAVE"]);
                                        parte.Representante = reader["REPRESENTANTE"] is DBNull ? string.Empty : Convert.ToString(reader["REPRESENTANTE"]);
                                        parte.Siglas = reader["SIGLAS"] is DBNull ? string.Empty : Convert.ToString(reader["SIGLAS"]);
                                        parte.PaisId = reader["ID_PAIS"] is DBNull ? string.Empty : Convert.ToString(reader["ID_PAIS"]);
                                        parte.EntidadId = reader["ID_ENTIDAD"] is DBNull ? 0 : Convert.ToInt32(reader["ID_ENTIDAD"]);
                                        parte.GobiernoId = reader["ID_GOBIERNO"] is DBNull ? char.MinValue : Convert.ToChar(reader["ID_GOBIERNO"]);

                                        // Add the element to the list.
                                        parteList.Add(parte);
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

            return parteList;
        }

        /// <summary>
        /// Gets the TIPO area list from the database. 
        /// </summary>
        /// <returns>The list of TIPO areas.</returns>
        public IEnumerable<ETipoArea> GetTipoArea()
        {
            // The expected model list.
            var tipoAreaList = new List<ETipoArea>();

            // The SQL stored procedure name.
            const string StoredProcedureName = @"USP_TIPO_AREA_SELECT";

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
                                        var tipoArea = new ETipoArea();

                                        tipoArea.TipoAreaId = reader["ID_TIPO_AREA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_TIPO_AREA"]);
                                        tipoArea.TipoArea = reader["TIPO_AREA"] is DBNull ? string.Empty : Convert.ToString(reader["TIPO_AREA"]);

                                        // Add the element to the list.
                                        tipoAreaList.Add(tipoArea);
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

            return tipoAreaList;
        }
        #endregion

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
            convenio.Estatus = reader["ESTATUS"] is DBNull ? char.MinValue : Convert.ToChar(reader["ESTATUS"]);
            convenio.RutaDocumento = reader["RUTA_DOCUMENTO"] is DBNull ? string.Empty : Convert.ToString(reader["RUTA_DOCUMENTO"]);
            convenio.NombreDocumento = string.IsNullOrEmpty(convenio.RutaDocumento) ? string.Empty : convenio.RutaDocumento.Substring(convenio.RutaDocumento.LastIndexOf('\\') + 1);
            convenio.Comentarios = reader["COMENTARIOS"] is DBNull ? string.Empty : Convert.ToString(reader["COMENTARIOS"]);

            return convenio;
        }

        /// <summary>
        /// Gets the CONVENIO Area.
        /// </summary>
        /// <param name="convenioId">The CONVENIO identifier.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A list of CONVENIO area.</returns>
        private IEnumerable<EConvenioArea> GetsConvenioArea(int convenioId, IDbConnection connection)
        {
            const string ConvenioAreaStoredProcedureName = @"USP_CONVENIO_AREA_SELECT_BY_ID";
            var areas = new List<EConvenioArea>();

            // Create database command object.
            using (var commandAreas = this.databaseHelper.CreateStoredProcDbCommand(ConvenioAreaStoredProcedureName, connection))
            {
                // Clear the parameters.
                commandAreas.Parameters.Clear();

                // Add the parameters to the list.
                commandAreas.Parameters.Add(this.databaseHelper.CreateParameter("idConvenio", OracleDbType.Int32, convenioId));
                commandAreas.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                // Execute the reader.
                using (var readerAreas = commandAreas.ExecuteReader())
                {
                    // Read the data rows.
                    while (readerAreas.Read())
                    {
                        var convenioArea = new EConvenioArea();

                        convenioArea.ConvenioId = readerAreas["ID_CONVENIO"] is DBNull ? 0 : Convert.ToInt32(readerAreas["ID_CONVENIO"]);
                        convenioArea.AreaId = readerAreas["ID_AREA"] is DBNull ? 0 : Convert.ToInt32(readerAreas["ID_AREA"]);
                        convenioArea.TipoAreaId = readerAreas["ID_TIPO_AREA"] is DBNull ? 0 : Convert.ToInt32(readerAreas["ID_TIPO_AREA"]);

                        // Fill the Area.
                        convenioArea.Area = new EArea();
                        convenioArea.Area.AreaId = readerAreas["ID_AREA"] is DBNull ? 0 : Convert.ToInt32(readerAreas["ID_AREA"]);
                        convenioArea.Area.Ramo = readerAreas["RAMO"] is DBNull ? (int?)null : Convert.ToInt32(readerAreas["RAMO"]);
                        convenioArea.Area.Ur = readerAreas["UR"] is DBNull ? string.Empty : Convert.ToString(readerAreas["UR"]);
                        convenioArea.Area.Area = readerAreas["AREA"] is DBNull ? string.Empty : Convert.ToString(readerAreas["AREA"]);

                        // Fill the tipo Area.
                        convenioArea.TipoArea = new ETipoArea();
                        convenioArea.TipoArea.TipoAreaId = readerAreas["ID_TIPO_AREA"] is DBNull ? 0 : Convert.ToInt32(readerAreas["ID_TIPO_AREA"]);
                        convenioArea.TipoArea.TipoArea = readerAreas["TIPO_AREA"] is DBNull ? string.Empty : Convert.ToString(readerAreas["TIPO_AREA"]);

                        areas.Add(convenioArea);
                    }
                }
            }

            return areas;
        }

        /// <summary>
        /// Inserts the CONVENIO Area.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A value indicating whether the value was inserted or not.</returns>
        private bool InsertConvenioArea(EConvenio request, IDbConnection connection)
        {
            var inserted = false;

            // The SQL stored procedure name.
            const string ConvenioAreaInsert = @"USP_CONVENIO_AREA_INSERT";

            foreach (var convenioArea in request.Areas)
            {
                // Create database command object.
                using (var commandArea = this.databaseHelper.CreateStoredProcDbCommand(ConvenioAreaInsert, connection))
                {
                    // Clear the parameters.
                    commandArea.Parameters.Clear();

                    // Add the parameters to the list.
                    commandArea.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, request.ConvenioId));
                    commandArea.Parameters.Add(this.databaseHelper.CreateParameter("areaId", OracleDbType.Int32, convenioArea.AreaId));
                    commandArea.Parameters.Add(this.databaseHelper.CreateParameter("tipoAreaId", OracleDbType.Int32, convenioArea.TipoAreaId));
                    commandArea.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                    // Execute the stored procedure.
                    commandArea.ExecuteNonQuery();

                    var affected = ((IDataParameter)commandArea.Parameters["affectedRows"]).Value == DBNull.Value ||
                         ((IDataParameter)commandArea.Parameters["affectedRows"]).Value == null
                         ? 0
                         : Convert.ToInt32(((IDataParameter)commandArea.Parameters["affectedRows"]).Value.ToString());

                    if (affected > 0)
                    {
                        inserted = true;
                    }
                }
            }

            return inserted;
        }

        /// <summary>
        /// Deletes the CONVENIO Area.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A value indicating whether the value was deleted or not.</returns>
        private bool DeleteConvenioArea(EConvenio request, IDbConnection connection)
        {
            var deleted = false;

            // The SQL stored procedure name.
            const string ConvenioAreaDelete = @"USP_CONVENIO_AREA_DELETE";

            // Create database command object.
            using (var commandArea = this.databaseHelper.CreateStoredProcDbCommand(ConvenioAreaDelete, connection))
            {
                // Clear the parameters.
                commandArea.Parameters.Clear();

                // Add the parameters to the list.
                commandArea.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, request.ConvenioId));
                commandArea.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                // Execute the stored procedure.
                commandArea.ExecuteNonQuery();

                var affected = ((IDataParameter)commandArea.Parameters["affectedRows"]).Value == DBNull.Value ||
                     ((IDataParameter)commandArea.Parameters["affectedRows"]).Value == null
                     ? 0
                     : Convert.ToInt32(((IDataParameter)commandArea.Parameters["affectedRows"]).Value.ToString());

                if (affected > 0)
                {
                    deleted = true;
                }
            }

            return deleted;
        }

        /// <summary>
        /// Gets the CONVENIO PARTE.
        /// </summary>
        /// <param name="convenioId">The CONVENIO identifier.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A list of CONVENIO PARTE.</returns>
        private IEnumerable<EConvenioParte> GetsConvenioParte(int convenioId, IDbConnection connection)
        {
            const string ConvenioParteStoredProcedureName = @"USP_CONVENIO_PARTE_SELECTBYID";
            var partes = new List<EConvenioParte>();

            // Create database command object.
            using (var commandPartes = this.databaseHelper.CreateStoredProcDbCommand(ConvenioParteStoredProcedureName, connection))
            {
                // Clear the parameters.
                commandPartes.Parameters.Clear();

                // Add the parameters to the list.
                commandPartes.Parameters.Add(this.databaseHelper.CreateParameter("idConvenio", OracleDbType.Int32, convenioId));
                commandPartes.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                // Execute the reader.
                using (var readerPartes = commandPartes.ExecuteReader())
                {
                    // Read the data rows.
                    while (readerPartes.Read())
                    {
                        var convenioParte = new EConvenioParte();

                        convenioParte.ConvenioId = readerPartes["ID_CONVENIO"] is DBNull ? 0 : Convert.ToInt32(readerPartes["ID_CONVENIO"]);
                        convenioParte.ParteId = readerPartes["ID_PARTE"] is DBNull ? 0 : Convert.ToInt32(readerPartes["ID_PARTE"]);
                        convenioParte.Representante = readerPartes["REPRESENTANTE"] is DBNull ? string.Empty : Convert.ToString(readerPartes["REPRESENTANTE"]);
                        convenioParte.Telefono = readerPartes["TELEFONOS"] is DBNull ? string.Empty : Convert.ToString(readerPartes["TELEFONOS"]);
                        convenioParte.CorreoElectronico = readerPartes["EMAIL"] is DBNull ? string.Empty : Convert.ToString(readerPartes["EMAIL"]);
                        convenioParte.Domicilio = readerPartes["DOMICILIO"] is DBNull ? string.Empty : Convert.ToString(readerPartes["DOMICILIO"]);
                        convenioParte.Cargo = readerPartes["CARGO"] is DBNull ? string.Empty : Convert.ToString(readerPartes["CARGO"]);

                        // Add the PARTE model
                        convenioParte.Parte = new EParte();

                        convenioParte.Parte.ParteId = readerPartes["ID_PARTE"] is DBNull ? 0 : Convert.ToInt32(readerPartes["ID_PARTE"]);
                        convenioParte.Parte.Parte = readerPartes["PARTE"] is DBNull ? string.Empty : Convert.ToString(readerPartes["PARTE"]);
                        convenioParte.Parte.Clave = readerPartes["CLAVE"] is DBNull ? string.Empty : Convert.ToString(readerPartes["CLAVE"]);
                        convenioParte.Parte.Siglas = readerPartes["SIGLAS"] is DBNull ? string.Empty : Convert.ToString(readerPartes["SIGLAS"]);
                        convenioParte.Parte.PaisId = readerPartes["ID_PAIS"] is DBNull ? string.Empty : Convert.ToString(readerPartes["ID_PAIS"]);
                        convenioParte.Parte.EntidadId = readerPartes["ID_ENTIDAD"] is DBNull ? 0 : Convert.ToInt32(readerPartes["ID_ENTIDAD"]);
                        convenioParte.Parte.GobiernoId = readerPartes["ID_GOBIERNO"] is DBNull ? char.MinValue : Convert.ToChar(readerPartes["ID_GOBIERNO"]);

                        partes.Add(convenioParte);
                    }
                }
            }

            return partes;
        }

        /// <summary>
        /// Inserts the CONVENIO PARTE.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A value indicating whether the value was inserted or not.</returns>
        private bool InsertConvenioParte(EConvenio request, IDbConnection connection)
        {
            var inserted = false;

            // The SQL stored procedure name.
            const string ConvenioParteInsert = @"USP_CONVENIO_PARTE_INSERT";

            foreach (var convenioParte in request.Partes)
            {
                // Create database command object.
                using (var commandParte = this.databaseHelper.CreateStoredProcDbCommand(ConvenioParteInsert, connection))
                {
                    // Clear the parameters.
                    commandParte.Parameters.Clear();

                    // Add the parameters to the list.
                    commandParte.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, request.ConvenioId));
                    commandParte.Parameters.Add(this.databaseHelper.CreateParameter("parteId", OracleDbType.Int32, convenioParte.ParteId));
                    commandParte.Parameters.Add(this.databaseHelper.CreateParameter("representante", OracleDbType.Varchar2, convenioParte.Representante));
                    commandParte.Parameters.Add(this.databaseHelper.CreateParameter("telefonos", OracleDbType.Varchar2, convenioParte.Telefono));
                    commandParte.Parameters.Add(this.databaseHelper.CreateParameter("eMail", OracleDbType.Varchar2, convenioParte.CorreoElectronico));
                    commandParte.Parameters.Add(this.databaseHelper.CreateParameter("domicilio", OracleDbType.Varchar2, convenioParte.Domicilio));
                    commandParte.Parameters.Add(this.databaseHelper.CreateParameter("cargo", OracleDbType.Varchar2, convenioParte.Cargo));
                    commandParte.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                    // Execute the stored procedure.
                    commandParte.ExecuteNonQuery();

                    var affected = ((IDataParameter)commandParte.Parameters["affectedRows"]).Value == DBNull.Value ||
                                             ((IDataParameter)commandParte.Parameters["affectedRows"]).Value == null
                                             ? 0
                                             : Convert.ToInt32(((IDataParameter)commandParte.Parameters["affectedRows"]).Value.ToString());

                    if (affected > 0)
                    {
                        inserted = true;
                    }
                }
            }

            return inserted;
        }

        /// <summary>
        /// Deletes the CONVENIO PARTE.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A value indicating whether the value was deleted or not.</returns>
        private bool DeleteConvenioParte(EConvenio request, IDbConnection connection)
        {
            var deleted = false;

            // The SQL stored procedure name.
            const string ConvenioParteDelete = @"USP_CONVENIO_PARTE_DELETE";

            // Create database command object.
            using (var commandParte = this.databaseHelper.CreateStoredProcDbCommand(ConvenioParteDelete, connection))
            {
                // Clear the parameters.
                commandParte.Parameters.Clear();

                // Add the parameters to the list.
                commandParte.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, request.ConvenioId));
                commandParte.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                // Execute the stored procedure.
                commandParte.ExecuteNonQuery();

                var affected = ((IDataParameter)commandParte.Parameters["affectedRows"]).Value == DBNull.Value ||
                                         ((IDataParameter)commandParte.Parameters["affectedRows"]).Value == null
                                         ? 0
                                         : Convert.ToInt32(((IDataParameter)commandParte.Parameters["affectedRows"]).Value.ToString());

                if (affected > 0)
                {
                    deleted = true;
                }
            }

            return deleted;
        }

        /// <summary>
        /// Gets the CONVENIO COMPROMISO.
        /// </summary>
        /// <param name="convenioId">The CONVENIO identifier.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A list of CONVENIO COMPROMISO.</returns>
        private IEnumerable<ECompromiso> GetsConvenioCompromiso(int convenioId, IDbConnection connection)
        {
            const string ConvenioCompromisoStoredProcedureName = @"USP_CONVENIO_COMPROMISO_BYID";
            var compromisos = new List<ECompromiso>();

            // Create database command object.
            using (var commandCompromiso = this.databaseHelper.CreateStoredProcDbCommand(ConvenioCompromisoStoredProcedureName, connection))
            {
                // Clear the parameters.
                commandCompromiso.Parameters.Clear();

                // Add the parameters to the list.
                commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("idConvenio", OracleDbType.Int32, convenioId));
                commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                // Execute the reader.
                using (var readerCompromiso = commandCompromiso.ExecuteReader())
                {
                    // Read the data rows.
                    while (readerCompromiso.Read())
                    {
                        var convenioCompromiso = new ECompromiso();

                        convenioCompromiso.ConvenioId = readerCompromiso["ID_CONVENIO"] is DBNull ? 0 : Convert.ToInt32(readerCompromiso["ID_CONVENIO"]);
                        convenioCompromiso.CompromisoId = readerCompromiso["ID_COMPROMISO"] is DBNull ? 0 : Convert.ToInt32(readerCompromiso["ID_COMPROMISO"]);
                        convenioCompromiso.Compromiso = readerCompromiso["COMPROMISO"] is DBNull ? string.Empty : Convert.ToString(readerCompromiso["COMPROMISO"]);
                        convenioCompromiso.FechaCompromiso = readerCompromiso["FECHA_COMPROMISO"] is DBNull ? string.Empty : Convert.ToString(readerCompromiso["FECHA_COMPROMISO"]);
                        convenioCompromiso.Avance = readerCompromiso["AVANCE"] is DBNull ? 0 : Convert.ToInt32(readerCompromiso["AVANCE"]);
                        convenioCompromiso.Ponderacion = readerCompromiso["PONDERACION"] is DBNull ? 0 : Convert.ToInt32(readerCompromiso["PONDERACION"]);

                        // Get the compromiso area
                        const string CompromisoParteStoredProcedureName = @"USP_COMPROMISO_PARTE_BY_ID";

                        // Add the PARTE model
                        convenioCompromiso.Parte = new EParte();

                        // Create database command object.
                        using (var commandCompromisoParte = this.databaseHelper.CreateStoredProcDbCommand(CompromisoParteStoredProcedureName, connection))
                        {
                            // Clear the parameters.
                            commandCompromisoParte.Parameters.Clear();

                            // Add the parameters to the list.
                            commandCompromisoParte.Parameters.Add(this.databaseHelper.CreateParameter("idCompromiso", OracleDbType.Int32, convenioCompromiso.CompromisoId));
                            commandCompromisoParte.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                            // Execute the reader.
                            using (var readerCompromisoParte = commandCompromisoParte.ExecuteReader())
                            {
                                // Read the data rows.
                                if (readerCompromisoParte.Read())
                                {
                                    var parte = new EParte();

                                    parte.ParteId = readerCompromisoParte["ID_PARTE"] is DBNull ? 0 : Convert.ToInt32(readerCompromisoParte["ID_PARTE"]);
                                    parte.Parte = readerCompromisoParte["PARTE"] is DBNull ? string.Empty : Convert.ToString(readerCompromisoParte["PARTE"]);
                                    parte.Clave = readerCompromisoParte["CLAVE"] is DBNull ? string.Empty : Convert.ToString(readerCompromisoParte["CLAVE"]);
                                    parte.Siglas = readerCompromisoParte["SIGLAS"] is DBNull ? string.Empty : Convert.ToString(readerCompromisoParte["SIGLAS"]);
                                    parte.PaisId = readerCompromisoParte["ID_PAIS"] is DBNull ? string.Empty : Convert.ToString(readerCompromisoParte["ID_PAIS"]);
                                    parte.EntidadId = readerCompromisoParte["ID_ENTIDAD"] is DBNull ? 0 : Convert.ToInt32(readerCompromisoParte["ID_ENTIDAD"]);
                                    parte.GobiernoId = readerCompromisoParte["ID_GOBIERNO"] is DBNull ? char.MinValue : Convert.ToChar(readerCompromisoParte["ID_GOBIERNO"]);

                                    // Add the PARTE to the CONVENIO
                                    convenioCompromiso.Parte = parte;
                                }
                            }
                        }

                        // Get the compromiso area
                        const string CompromisoAreaStoredProcedureName = @"USP_COMPROMISO_AREA_BY_ID";

                        // Create database command object.
                        using (var commandCompromisoArea = this.databaseHelper.CreateStoredProcDbCommand(CompromisoAreaStoredProcedureName, connection))
                        {
                            // Clear the parameters.
                            commandCompromisoArea.Parameters.Clear();

                            // Add the parameters to the list.
                            commandCompromisoArea.Parameters.Add(this.databaseHelper.CreateParameter("idCompromiso", OracleDbType.Int32, convenioCompromiso.CompromisoId));
                            commandCompromisoArea.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                            // Execute the reader.
                            using (var dataReader = commandCompromisoArea.ExecuteReader())
                            {
                                var areasCompromiso = new List<EArea>();

                                // Read the data rows.
                                while (dataReader.Read())
                                {
                                    var area = new EArea();

                                    area.AreaId = dataReader["ID_AREA"] is DBNull ? 0 : Convert.ToInt32(dataReader["ID_AREA"]);
                                    area.Ramo = dataReader["RAMO"] is DBNull ? 0 : Convert.ToInt32(dataReader["RAMO"]);
                                    area.Ur = dataReader["UR"] is DBNull ? string.Empty : Convert.ToString(dataReader["UR"]);
                                    area.Area = dataReader["AREA"] is DBNull ? string.Empty : Convert.ToString(dataReader["AREA"]);
                                    area.Selected = !(dataReader["SELECTED"] is DBNull) && Convert.ToBoolean(dataReader["SELECTED"]);

                                    // Add the area to the list.
                                    areasCompromiso.Add(area);
                                }

                                // Add all the areas to the COMPROMISO.
                                convenioCompromiso.Areas = areasCompromiso;
                            }
                        }

                        // Add the COMPROMISO to the list.
                        compromisos.Add(convenioCompromiso);
                    }
                }
            }

            return compromisos;
        }

        /// <summary>
        /// Inserts the CONVENIO COMPROMISO.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A value indicating whether the value was inserted or not.</returns>
        private bool InsertConvenioCompromiso(EConvenio request, IDbConnection connection)
        {
            var inserted = false;

            // The SQL stored procedure name.
            const string ConvenioCompromisoInsert = @"USP_CONVENIO_COMPROMISO_INSERT";

            foreach (var compromiso in request.Compromisos)
            {
                // Create database command object.
                using (var commandCompromiso = this.databaseHelper.CreateStoredProcDbCommand(ConvenioCompromisoInsert, connection))
                {
                    // Clear the parameters.
                    commandCompromiso.Parameters.Clear();

                    var fechaCompromiso = !string.IsNullOrEmpty(compromiso.FechaCompromiso)
                                             ? DateTime.ParseExact(compromiso.FechaCompromiso, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                             : DateTime.MinValue;

                    // Add the parameters to the list.
                    commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, request.ConvenioId));
                    commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("compromiso", OracleDbType.Varchar2, compromiso.Compromiso));
                    commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("fechaCompromiso", OracleDbType.Date, fechaCompromiso == DateTime.MinValue ? (DateTime?)null : fechaCompromiso));
                    commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("avance", OracleDbType.Int32, compromiso.Avance));
                    commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("ponderacion", OracleDbType.Int32, compromiso.Ponderacion));
                    commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("compromisoId", OracleDbType.Int32, 0, ParameterDirection.Output));

                    commandCompromiso.ExecuteNonQuery();

                    compromiso.CompromisoId = ((IDataParameter)commandCompromiso.Parameters["compromisoId"]).Value == DBNull.Value ||
                                             ((IDataParameter)commandCompromiso.Parameters["compromisoId"]).Value == null
                                             ? 0
                                             : Convert.ToInt32(((IDataParameter)commandCompromiso.Parameters["compromisoId"]).Value.ToString());

                    if (compromiso.CompromisoId > 0)
                    {
                        // The SQL stored procedure name.
                        const string CompromisoParteInsert = @"USP_COMPROMISO_PARTE_INSERT";

                        // Create database command object.
                        using (var commandCompromisoParte = this.databaseHelper.CreateStoredProcDbCommand(CompromisoParteInsert, connection))
                        {
                            // Clear the parameters.
                            commandCompromisoParte.Parameters.Clear();

                            commandCompromisoParte.Parameters.Add(this.databaseHelper.CreateParameter("compromisoId", OracleDbType.Int32, compromiso.CompromisoId));
                            commandCompromisoParte.Parameters.Add(this.databaseHelper.CreateParameter("parteId", OracleDbType.Int32, compromiso.Parte.ParteId));
                            commandCompromisoParte.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                            // Execute the stored procedure.
                            commandCompromisoParte.ExecuteNonQuery();
                        }

                        // The SQL stored procedure name.
                        const string CompromisoAreaInsert = @"USP_COMPROMISO_AREA_INSERT";

                        // Insertar compromiso area aqui 
                        foreach (var compromisoArea in compromiso.Areas ?? new List<EArea>())
                        {
                            // Create database command object.
                            using (var commandCompromisoArea = this.databaseHelper.CreateStoredProcDbCommand(CompromisoAreaInsert, connection))
                            {
                                // Clear the parameters.
                                commandCompromisoArea.Parameters.Clear();

                                commandCompromisoArea.Parameters.Add(this.databaseHelper.CreateParameter("compromisoId", OracleDbType.Int32, compromiso.CompromisoId));
                                commandCompromisoArea.Parameters.Add(this.databaseHelper.CreateParameter("areaId", OracleDbType.Int32, compromisoArea.AreaId));
                                commandCompromisoArea.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                                // Execute the stored procedure.
                                commandCompromisoArea.ExecuteNonQuery();
                            }
                        }

                        inserted = true;
                    }
                }
            }

            return inserted;
        }

        /// <summary>
        /// Deletes the CONVENIO COMPROMISO.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A value indicating whether the value was deleted or not.</returns>
        private bool DeleteConvenioCompromiso(EConvenio request, IDbConnection connection)
        {
            var deleted = false;

            foreach (var compromiso in request.Compromisos)
            {
                // The SQL stored procedure name.
                const string CompromisoParteDelete = @"USP_COMPROMISO_PARTE_DELETE";

                // Create database command object.
                using (var commandCompromisoParte = this.databaseHelper.CreateStoredProcDbCommand(CompromisoParteDelete, connection))
                {
                    // Clear the parameters.
                    commandCompromisoParte.Parameters.Clear();

                    commandCompromisoParte.Parameters.Add(this.databaseHelper.CreateParameter("compromisoId", OracleDbType.Int32, compromiso.CompromisoId));
                    commandCompromisoParte.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                    // Execute the stored procedure.
                    commandCompromisoParte.ExecuteNonQuery();
                }

                // The SQL stored procedure name.
                const string CompromisoAreaDelete = @"USP_COMPROMISO_AREA_DELETE";

                // Create database command object.
                using (var commandCompromisoArea = this.databaseHelper.CreateStoredProcDbCommand(CompromisoAreaDelete, connection))
                {
                    // Clear the parameters.
                    commandCompromisoArea.Parameters.Clear();

                    commandCompromisoArea.Parameters.Add(this.databaseHelper.CreateParameter("compromisoId", OracleDbType.Int32, compromiso.CompromisoId));
                    commandCompromisoArea.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                    // Execute the stored procedure.
                    commandCompromisoArea.ExecuteNonQuery();
                }
            }

            // The SQL stored procedure name.
            const string ConvenioCompromisoDelete = @"USP_CONVENIO_COMPROMISO_DELET";

            // Create database command object.
            using (var commandCompromiso = this.databaseHelper.CreateStoredProcDbCommand(ConvenioCompromisoDelete, connection))
            {
                // Clear the parameters.
                commandCompromiso.Parameters.Clear();

                // Add the parameters to the list.
                commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("convenioId", OracleDbType.Int32, request.ConvenioId));
                commandCompromiso.Parameters.Add(this.databaseHelper.CreateParameter("affectedRows", OracleDbType.Int32, 0, ParameterDirection.Output));

                commandCompromiso.ExecuteNonQuery();
            }

            return deleted;
        }

        /// <summary>
        /// Gets the CONVENIO PARTE.
        /// </summary>
        /// <param name="compromisos">The COMPROMISOS identifier string.</param>
        /// <param name="connection">The current connection.</param>
        /// <returns>A list of CONVENIO PARTE.</returns>
        private IEnumerable<EParte> GetsPartesCompromiso(string compromisos, IDbConnection connection)
        {
            const string PartesCompromiso = @"USP_PARTES_COMPROMISO";
            var partes = new List<EParte>();

            // Create database command object.
            using (var commandPartes = this.databaseHelper.CreateStoredProcDbCommand(PartesCompromiso, connection))
            {
                // Clear the parameters.
                commandPartes.Parameters.Clear();

                // Add the parameters to the list.
                commandPartes.Parameters.Add(this.databaseHelper.CreateParameter("csvIdCompromiso", OracleDbType.Varchar2, compromisos));
                commandPartes.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                // Execute the reader.
                using (var readerPartes = commandPartes.ExecuteReader())
                {
                    // Read the data rows.
                    while (readerPartes.Read())
                    {
                        // Add the PARTE model
                        var parte = new EParte();

                        parte.ParteId = readerPartes["ID_PARTE"] is DBNull ? 0 : Convert.ToInt32(readerPartes["ID_PARTE"]);
                        parte.Parte = readerPartes["PARTE"] is DBNull ? string.Empty : Convert.ToString(readerPartes["PARTE"]);
                        parte.Clave = readerPartes["CLAVE"] is DBNull ? string.Empty : Convert.ToString(readerPartes["CLAVE"]);
                        parte.Siglas = readerPartes["SIGLAS"] is DBNull ? string.Empty : Convert.ToString(readerPartes["SIGLAS"]);
                        parte.PaisId = readerPartes["ID_PAIS"] is DBNull ? string.Empty : Convert.ToString(readerPartes["ID_PAIS"]);
                        parte.EntidadId = readerPartes["ID_ENTIDAD"] is DBNull ? 0 : Convert.ToInt32(readerPartes["ID_ENTIDAD"]);
                        parte.GobiernoId = readerPartes["ID_GOBIERNO"] is DBNull ? char.MinValue : Convert.ToChar(readerPartes["ID_GOBIERNO"]);

                        partes.Add(parte);
                    }
                }
            }

            return partes;
        }
        #endregion
    }
}
