//-----------------------------------------------------------------------
// <copyright file="IDatabaseHelper.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Utilities
{
    using System.Data;
    using Oracle.ManagedDataAccess.Client;

    /// <summary>
    /// The database access for creating database connections, commands and parameters.
    /// </summary>
    public interface IDatabaseHelper
    {
        /// <summary>
        /// Creates the database connection.
        /// </summary>
        /// <returns>
        /// An <see cref="IDbConnection"/> Database Connection.
        /// </returns>
        IDbConnection CreateDbConnection();

        /// <summary>
        /// Creates a database command using the specified values.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns>
        /// An <see cref="IDbCommand"/> Database Command.
        /// </returns>
        IDbCommand CreateDbCommand(string commandText);

        /// <summary>
        /// Creates a database command using the specified values.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="databaseConnection">The database connection.</param>
        /// <returns>
        /// An <see cref="IDbCommand"/> Database Command.
        /// </returns>
        IDbCommand CreateDbCommand(string commandText, IDbConnection databaseConnection);

        /// <summary>
        /// Creates a database command for stored procedures using the specified values.
        /// </summary>
        /// <param name="storedProcedureName">The stored procedure name (command text).</param>
        /// <param name="databaseConnection">The database connection.</param>
        /// <returns>
        /// An <see cref="IDbCommand"/> Database Command.
        /// </returns>
        IDbCommand CreateStoredProcDbCommand(string storedProcedureName, IDbConnection databaseConnection);

        /// <summary>
        /// Creates a database parameter using the specified values.
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterType">The parameter type.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <param name="parameterDirection">The parameter direction.</param>
        /// <returns>
        ///  An <see cref="IDbDataParameter"/> Database Parameter.
        /// </returns>
        IDbDataParameter CreateParameter(string parameterName, OracleDbType parameterType, object parameterValue, ParameterDirection parameterDirection = ParameterDirection.Input);
    }
}
