//-----------------------------------------------------------------------
// <copyright file="DatabaseHelper.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Utilities
{
    using System.Data;
    using Oracle.ManagedDataAccess.Client;

    /// <summary>
    /// The database utility implementation for creating database connections, commands and parameters.
    /// </summary>
    public class DatabaseHelper : IDatabaseHelper
    {
        /// <summary>
        /// Creates a new oracle database connection.
        /// </summary>
        /// <returns>
        /// An <see cref="IDbConnection"/> Database Connection.
        /// </returns>
        public IDbConnection CreateDbConnection()
        {
            //// TODO: Check if we can add more parameters to the database connection.
            return new OracleConnection();
        }

        /// <summary>
        /// Creates a database command using the specified values.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns>
        /// An <see cref="IDbCommand"/> Database Command.
        /// </returns>
        public IDbCommand CreateDbCommand(string commandText)
        {
            var oracleCommand = new OracleCommand(commandText);
            oracleCommand.BindByName = true;

            return oracleCommand;
        }

        /// <summary>
        /// Creates a database command using the specified values.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="databaseConnection">The database connection.</param>
        /// <returns>
        /// An <see cref="IDbCommand"/> Database Command.
        /// </returns>
        public IDbCommand CreateDbCommand(string commandText, IDbConnection databaseConnection)
        {
            var oracleCommand = new OracleCommand(commandText, (OracleConnection)databaseConnection);
            oracleCommand.BindByName = true;

            return oracleCommand;
        }

        /// <summary>
        /// Creates a database command for stored procedures using the specified values.
        /// </summary>
        /// <param name="storedProcedureName">The stored procedure name (command text).</param>
        /// <param name="databaseConnection">The database connection.</param>
        /// <returns>
        /// An <see cref="IDbCommand"/> Database Command.
        /// </returns>
        public IDbCommand CreateStoredProcDbCommand(string storedProcedureName, IDbConnection databaseConnection)
        {
            var oracleCommand = new OracleCommand(storedProcedureName, (OracleConnection)databaseConnection);
            oracleCommand.BindByName = true;
            oracleCommand.CommandText = storedProcedureName;
            oracleCommand.CommandType = CommandType.StoredProcedure;

            return oracleCommand;
        }

        /// <summary>
        /// Creates a database parameter using the specified values.
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns>
        ///  An <see cref="IDbDataParameter"/> Database Parameter.
        /// </returns>
        public IDbDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            var oracleParameter = new OracleParameter(parameterName, parameterValue);

            return oracleParameter;
        }

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
        public IDbDataParameter CreateParameter(string parameterName, OracleDbType parameterType, object parameterValue, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            var oracleParameter = new OracleParameter(parameterName, parameterType, parameterValue, parameterDirection);

            return oracleParameter;
        }
    }
}
