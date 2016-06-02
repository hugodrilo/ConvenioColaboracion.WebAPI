//-----------------------------------------------------------------------
// <copyright file="DbCompromisoService.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    using System;
    using System.Configuration;
    using System.Data;
    using Entities.Models.Request;
    using Oracle.ManagedDataAccess.Client;
    using Utilities;

    /// <summary>
    /// The COMPROMISO database service implementation.
    /// </summary>
    public class DbCompromisoService : IDbCompromisoService
    {
        /// <summary>
        /// The data access factory
        /// </summary>
        private readonly IDatabaseHelper databaseHelper;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCompromisoService"/> class. 
        /// </summary>
        /// <param name="databaseHelper">The database helper.</param>
        public DbCompromisoService(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        #endregion
        /// <summary>
        /// Gets the COMPROMISO from the database. 
        /// </summary>
        /// <param name="compromisoId">The COMPROMISO identifier.</param>
        /// <returns>Expected COMPROMISO model.</returns>
        public ECompromiso GetCompromiso(int compromisoId)
        {
            var compromiso = new ECompromiso();

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
                            const string StoredProcedureName = @"USP_COMPROMISO_SELECT_BY_ID";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("idCompromiso", OracleDbType.Varchar2, compromisoId));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    if (reader.Read())
                                    {
                                        compromiso = GetCompromisoFromReader(reader);
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

            return compromiso;
        }

        #region Auxiliar Methods
        /// <summary>
        /// Gets the CONVENIO from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected CONVENIO model</returns>
        private static ECompromiso GetCompromisoFromReader(IDataReader reader)
        {
            var compromiso = new ECompromiso();

            compromiso.CompromisoId = reader["ID_COMPROMISO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_COMPROMISO"]);
            compromiso.ConvenioId = reader["ID_CONVENIO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_CONVENIO"]);
            compromiso.NumeroCompromiso = reader["NUMERO_COMPROMISO"] is DBNull ? 0 : Convert.ToInt32(reader["NUMERO_COMPROMISO"]);
            compromiso.Compromiso = reader["COMPROMISO"] is DBNull ? string.Empty : Convert.ToString(reader["COMPROMISO"]);
            compromiso.FechaCompromiso = reader["FECHA_COMPROMISO"] is DBNull ? string.Empty : Convert.ToString(reader["FECHA_COMPROMISO"]);
            compromiso.Avance = reader["AVANCE"] is DBNull ? 0 : Convert.ToInt32(reader["AVANCE"]);
            compromiso.Ponderacion = reader["PONDERACION"] is DBNull ? 0 : Convert.ToInt32(reader["PONDERACION"]);

            return compromiso;
        }
        #endregion
    }
}
