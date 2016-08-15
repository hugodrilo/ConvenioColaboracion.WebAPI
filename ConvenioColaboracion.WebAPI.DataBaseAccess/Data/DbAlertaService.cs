//-----------------------------------------------------------------------
// <copyright file="DbAlertaService.cs" company="SFP">
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
    /// The ALERTA database service implementation.
    /// </summary>
    public class DbAlertaService : IDbAlertaService
    {
        /// <summary>
        /// The data access factory
        /// </summary>
        private readonly IDatabaseHelper databaseHelper;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DbAlertaService"/> class. 
        /// </summary>
        /// <param name="databaseHelper">The database helper.</param>
        public DbAlertaService(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        #endregion

        /// <summary>
        /// Gets all the ALERTAS from the database that are inactive for a period of time. 
        /// </summary>
        /// <param name="alertaRequest">The ALERTA request model.</param>
        /// <returns>Expected list of ALERTAS.</returns>
        public IEnumerable<Entities.Models.Return.EAlerta> GetAlertaInactividad(EAlerta alertaRequest)
        {
            var alertaList = new List<Entities.Models.Return.EAlerta>();

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
                            const string StoredProcedureName = @"USP_ALERTA_INACTIVIDAD";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_PAGINA", OracleDbType.Int32, alertaRequest.Pagina));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_REGISTROS", OracleDbType.Int32, alertaRequest.Registro));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_MESES", OracleDbType.Int32, alertaRequest.Meses));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("CUR_RESULTADO", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var alerta = GetAlertaFromReader(reader);

                                        alertaList.Add(alerta);
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

            return alertaList;
        }

        /// <summary>
        /// Gets all the ALERTAS from the database that are going to expire. 
        /// </summary>
        /// <param name="alertaRequest">The ALERTA request model.</param>
        /// <returns>Expected list of ALERTAS.</returns>
        public IEnumerable<Entities.Models.Return.EAlerta> GetAlertaVencimiento(EAlerta alertaRequest)
        {
            var alertaList = new List<Entities.Models.Return.EAlerta>();

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
                            const string StoredProcedureName = @"USP_ALERTA_VENCIMIENTO";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_PAGINA", OracleDbType.Int32, alertaRequest.Pagina));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_REGISTROS", OracleDbType.Int32, alertaRequest.Registro));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_MESES", OracleDbType.Int32, alertaRequest.Meses));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("CUR_RESULTADO", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    while (reader.Read())
                                    {
                                        var alerta = GetAlertaFromReader(reader);

                                        alertaList.Add(alerta);
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

            return alertaList;
        }

        #region Auxiliar Methods
        /// <summary>
        /// Gets the ALERTA from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected ALERTA model.</returns>
        private static Entities.Models.Return.EAlerta GetAlertaFromReader(IDataReader reader)
        {
            var alerta = new Entities.Models.Return.EAlerta();

            alerta.Id = reader["ID"] is DBNull ? 0 : Convert.ToInt32(reader["ID"]);
            alerta.ConvenioId = reader["ID_CONVENIO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_CONVENIO"]);
            alerta.Año = reader["AÑO"] is DBNull ? 0 : Convert.ToInt32(reader["AÑO"]);
            alerta.NumeroConvenio = reader["NUMERO_CONVENIO"] is DBNull ? string.Empty : Convert.ToString(reader["NUMERO_CONVENIO"]);
            alerta.Convenio = reader["CONVENIO"] is DBNull ? string.Empty : Convert.ToString(reader["CONVENIO"]);
            alerta.NombreCorto = reader["NOMBRE_CORTO"] is DBNull ? string.Empty : Convert.ToString(reader["NOMBRE_CORTO"]);
            alerta.Resumen = reader["RESUMEN"] is DBNull ? string.Empty : Convert.ToString(reader["RESUMEN"]);
            alerta.FechaSuscripcion = reader["FECHA_SUSCRIPCION"] is DBNull ? string.Empty : Convert.ToString(reader["FECHA_SUSCRIPCION"]);
            alerta.FechaTermino = reader["FECHA_TERMINO"] is DBNull ? string.Empty : Convert.ToString(reader["FECHA_TERMINO"]);
            alerta.MateriaId = reader["ID_MATERIA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_MATERIA"]);
            alerta.Materia = reader["MATERIA"] is DBNull ? string.Empty : Convert.ToString(reader["MATERIA"]);
            alerta.SexenioId = reader["ID_SEXENIO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_SEXENIO"]);
            alerta.Avance = reader["AVANCE"] is DBNull ? 0 : Convert.ToInt32(reader["AVANCE"]);
            alerta.Estatus = reader["ESTATUS"] is DBNull ? 0 : Convert.ToInt32(reader["ESTATUS"]);
            alerta.RutaDocumento = reader["RUTA_DOCUMENTO"] is DBNull ? string.Empty : Convert.ToString(reader["RUTA_DOCUMENTO"]);
            alerta.Comentarios = reader["COMENTARIOS"] is DBNull ? string.Empty : Convert.ToString(reader["COMENTARIOS"]);
            alerta.Logo = reader["LOGO"] is DBNull ? string.Empty : Convert.ToString(reader["LOGO"]);
            alerta.UltimoReporte = reader["ULTIMO_REPORTE"] is DBNull ? string.Empty : Convert.ToString(reader["ULTIMO_REPORTE"]);
            ////alerta.FechaCompromiso = reader["FECHA_COMPROMISO"] is DBNull ? string.Empty : Convert.ToString(reader["FECHA_COMPROMISO"]);
            alerta.AreaVinculante = reader["AREA_VINCULANTE"] is DBNull ? string.Empty : Convert.ToString(reader["AREA_VINCULANTE"]);

            return alerta;
        }
        #endregion
    }
}
