//-----------------------------------------------------------------------
// <copyright file="DbEstadisticaService.cs" company="SFP">
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
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Utilities;
    using ConvenioColaboracion.WebAPI.Entities.Models.Request;
    using Oracle.ManagedDataAccess.Client;

    /// <summary>
    /// The database ESTADISTICA service implementation.
    /// </summary>
    public class DbEstadisticaService : IDbEstadisticaService
    {
        /// <summary>
        /// The data access factory
        /// </summary>
        private readonly IDatabaseHelper databaseHelper;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DbEstadisticaService"/> class. 
        /// </summary>
        /// <param name="databaseHelper">The database helper.</param>
        public DbEstadisticaService(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        #endregion

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected list of ADMINISTRACION.</returns>
        public IEnumerable<Entities.Models.Return.EInformePeriodo> GetInformePeriodo(EInformePeriodo request)
        {
            var informePeriodoList = new List<Entities.Models.Return.EInformePeriodo>();

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
                            const string StoredProcedureName = @"USP_INFORME_ADMON_MATERIA";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_ID_ADMON", OracleDbType.Int32, request.AdministracionId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_ID_MATERIA", OracleDbType.Int32, request.MateriaId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_ID_YEAR", OracleDbType.Int32, request.AñoId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_GRP_ADMON", OracleDbType.Int32, request.AgruparAdministracion));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_GRP_MATERIA", OracleDbType.Int32, request.AgruparMateria));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_GRP_YEAR", OracleDbType.Int32, request.AgruparAño));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("CUR_RESULTADO", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var informe = GetInformeFromReader(reader);

                                        informePeriodoList.Add(informe);
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

            return informePeriodoList;
        }

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected list of MATERIAS.</returns>
        public IEnumerable<Entities.Models.Return.EInformeMateria> GetInformeMateria(EInformeMateria request)
        {
            var informeMateriaList = new List<Entities.Models.Return.EInformeMateria>();

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
                            const string StoredProcedureName = @"USP_INFORME_MATERIA";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_ID_ADMON", OracleDbType.Int32, request.AdministracionId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_ID_MATERIA", OracleDbType.Int32, request.MateriaId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_ID_SUBMATERIA", OracleDbType.Int32, request.SubMateriaId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_GRP_ADMON", OracleDbType.Int32, request.AgruparAdministracion));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_GRP_MATERIA", OracleDbType.Int32, request.AgruparMateria));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_GRP_SUBMATERIA", OracleDbType.Int32, request.AgruparSubMateria));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("CUR_RESULTADO", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var informe = GetInformeMateriaFromReader(reader);

                                        informeMateriaList.Add(informe);
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

            return informeMateriaList;
        }

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected list of MATERIAS.</returns>
        public IEnumerable<Entities.Models.Return.EInformeArea> GetInformeArea(EInformeArea request)
        {
            var informeMateriaList = new List<Entities.Models.Return.EInformeArea>();

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
                            const string StoredProcedureName = @"USP_INFORME_AREA";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("CUR_RESULTADO", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var informe = GetInformeAreaFromReader(reader);

                                        informeMateriaList.Add(informe);
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

            return informeMateriaList;
        }

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected list of MATERIAS.</returns>
        public IEnumerable<Entities.Models.Return.EInformeGrado> GetInformeGrado(EInformeGrado request)
        {
            var informeMateriaList = new List<Entities.Models.Return.EInformeGrado>();

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
                            const string StoredProcedureName = @"USP_INFORME_CUMPLIMIENTO";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("CUR_RESULTADO", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var informe = GetInformeGradoFromReader(reader);

                                        informeMateriaList.Add(informe);
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

            return informeMateriaList;
        }

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="id">The SEXENIO identifier.</param>
        /// <returns>Expected SEXENIO model.</returns>
        public ESexenio GetSexenio(int id)
        {
            var sexenio = new ESexenio();

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
                            const string StoredProcedureName = @"USP_SEXENIO_SELECT_BY_ID";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("sexenioId", OracleDbType.Int32, id));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    if (reader.Read())
                                    {
                                        sexenio.SexenioId = reader["ID_SEXENIO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_SEXENIO"]);
                                        sexenio.Sexenio = reader["SEXENIO"] is DBNull ? string.Empty : Convert.ToString(reader["SEXENIO"]);
                                        sexenio.Desde = reader["DESDE"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["DESDE"]);
                                        sexenio.Hasta = reader["HASTA"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["HASTA"]);
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

            return sexenio;
        }

        /// <summary>
        /// Gets all the results from the database that matches the specified request parameters. 
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>Expected CONVENIO list.</returns>
        public IEnumerable<Entities.Models.Return.EBuscaConvenio> GetConvenio(EBuscaConvenio request)
        {
            var convenioList = new List<Entities.Models.Return.EBuscaConvenio>();

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
                            const string StoredProcedureName = @"USP_BUSCA_CONVENIOS";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();
                                var filtro = Newtonsoft.Json.JsonConvert.SerializeObject(request.Filtros);

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_PAGINA", OracleDbType.Int32, request.Pagina));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_REGISTROS", OracleDbType.Int32, request.Registros));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_KEYWORDS", OracleDbType.Varchar2, request.Keywords));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_FILTROS", OracleDbType.Varchar2, filtro));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_OPERACION", OracleDbType.Int32, request.OperacionId == null ? 1 : request.OperacionId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_USUARIO", OracleDbType.Int32, request.UsuarioId == null ? 0 : request.UsuarioId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("CUR_RESULTADO", OracleDbType.RefCursor, 0, ParameterDirection.Output));

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
        /// Gets the INFORME POR ADMINISTRACION from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected INFORME model.</returns>
        private static Entities.Models.Return.EInformePeriodo GetInformeFromReader(IDataReader reader)
        {
            var periodo = new Entities.Models.Return.EInformePeriodo();

            periodo.AdministracionId = reader["ID_ADMINISTRACION"] is DBNull ? 0 : Convert.ToInt32(reader["ID_ADMINISTRACION"]);
            periodo.Administracion = reader["ADMINISTRACION"] is DBNull ? string.Empty : Convert.ToString(reader["ADMINISTRACION"]);
            periodo.Año = reader["AÑO"] is DBNull ? 0 : Convert.ToInt32(reader["AÑO"]);
            periodo.MateriaId = reader["ID_MATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_MATERIA"]);
            periodo.Materia = reader["MATERIA"] is DBNull ? string.Empty : Convert.ToString(reader["MATERIA"]);
            periodo.NumeroConvenio = reader["CONVENIOS"] is DBNull ? 0 : Convert.ToInt32(reader["CONVENIOS"]);

            return periodo;
        }

        /// <summary>
        /// Gets the INFORME POR MATERIA from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected INFORME model.</returns>
        private static Entities.Models.Return.EInformeMateria GetInformeMateriaFromReader(IDataReader reader)
        {
            var materia = new Entities.Models.Return.EInformeMateria();

            materia.AdministracionId = reader["ID_ADMINISTRACION"] is DBNull ? 0 : Convert.ToInt32(reader["ID_ADMINISTRACION"]);
            materia.Administracion = reader["ADMINISTRACION"] is DBNull ? string.Empty : Convert.ToString(reader["ADMINISTRACION"]);
            materia.MateriaId = reader["ID_MATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_MATERIA"]);
            materia.SubMateriaId = reader["ID_SUBMATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_SUBMATERIA"]);
            materia.Materia = reader["MATERIA"] is DBNull ? string.Empty : Convert.ToString(reader["MATERIA"]);
            materia.SubMateria = reader["SUBMATERIA"] is DBNull ? string.Empty : Convert.ToString(reader["SUBMATERIA"]);
            materia.NumeroConvenio = reader["CONVENIOS"] is DBNull ? 0 : Convert.ToInt32(reader["CONVENIOS"]);

            return materia;
        }

        /// <summary>
        /// Gets the INFORME POR area from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected INFORME model.</returns>
        private static Entities.Models.Return.EInformeArea GetInformeAreaFromReader(IDataReader reader)
        {
            var materia = new Entities.Models.Return.EInformeArea();

            materia.Orden = reader["ORDEN"] is DBNull ? 0 : Convert.ToInt32(reader["ORDEN"]);
            materia.AreaId = reader["ID_AREA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_AREA"]);
            materia.Siglas = reader["SIGLAS"] is DBNull ? string.Empty : Convert.ToString(reader["SIGLAS"]);
            materia.Area = reader["AREA"] is DBNull ? string.Empty : Convert.ToString(reader["AREA"]);
            materia.ResponsableInstitucional = reader["RESPONSABLE_INSTITUCIONAL"] is DBNull ? string.Empty : Convert.ToString(reader["RESPONSABLE_INSTITUCIONAL"]);
            materia.ResponsableOperativo = reader["RESPONSABLE_OPERATIVO"] is DBNull ? string.Empty : Convert.ToString(reader["RESPONSABLE_OPERATIVO"]);

            return materia;
        }

        /// <summary>
        /// Gets the INFORME POR GRADO from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected INFORME model.</returns>
        private static Entities.Models.Return.EInformeGrado GetInformeGradoFromReader(IDataReader reader)
        {
            var materia = new Entities.Models.Return.EInformeGrado();

            materia.EstatusId = reader["ID_ESTATUS"] is DBNull ? 0 : Convert.ToInt32(reader["ID_ESTATUS"]);
            materia.Estatus = reader["ESTATUS"] is DBNull ? string.Empty : Convert.ToString(reader["ESTATUS"]);
            materia.NumeroConvenio = reader["CONVENIOS"] is DBNull ? 0 : Convert.ToInt32(reader["CONVENIOS"]);

            return materia;
        }

        /// <summary>
        /// Gets the CONVENIO from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected CONVENIO model</returns>
        private static Entities.Models.Return.EBuscaConvenio GetConvenioFromReader(IDataReader reader)
        {
            var convenio = new Entities.Models.Return.EBuscaConvenio();

            convenio.ConvenioId = reader["ID_CONVENIO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_CONVENIO"]);
            convenio.NumeroConvenio = reader["NUMERO_CONVENIO"] is DBNull ? string.Empty : Convert.ToString(reader["NUMERO_CONVENIO"]);
            convenio.Convenio = reader["CONVENIO"] is DBNull ? string.Empty : Convert.ToString(reader["CONVENIO"]);
            convenio.NombreCorto = reader["NOMBRE_CORTO"] is DBNull ? string.Empty : Convert.ToString(reader["NOMBRE_CORTO"]);
            convenio.Resumen = reader["RESUMEN"] is DBNull ? string.Empty : Convert.ToString(reader["RESUMEN"]);
            convenio.FechaSuscripcion = reader["FECHA_SUSCRIPCION"] is DBNull ? string.Empty : Convert.ToString(reader["FECHA_SUSCRIPCION"]);
            convenio.FechaTermino = reader["FECHA_TERMINO"] is DBNull ? string.Empty : Convert.ToString(reader["FECHA_TERMINO"]);
            convenio.Materia = reader["MATERIA"] is DBNull ? string.Empty : Convert.ToString(reader["MATERIA"]);
            convenio.SexenioId = reader["ID_SEXENIO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_SEXENIO"]);
            convenio.Avance = reader["AVANCE"] is DBNull ? 0 : Convert.ToInt32(reader["AVANCE"]);
            convenio.Estatus = reader["ESTATUS"] is DBNull ? 0 : Convert.ToInt32(reader["ESTATUS"]);
            convenio.RutaDocumento = reader["RUTA_DOCUMENTO"] is DBNull ? string.Empty : Convert.ToString(reader["RUTA_DOCUMENTO"]);
            convenio.Comentarios = reader["COMENTARIOS"] is DBNull ? string.Empty : Convert.ToString(reader["COMENTARIOS"]);
            convenio.Logo = reader["LOGO"] is DBNull ? string.Empty : Convert.ToString(reader["LOGO"]);

            return convenio;
        }

        #endregion
    }
}
