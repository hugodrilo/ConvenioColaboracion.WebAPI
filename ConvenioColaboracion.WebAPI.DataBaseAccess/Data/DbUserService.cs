//-----------------------------------------------------------------------
// <copyright file="DbUserService.cs" company="SFP">
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
    using Entities.Models.Return;
    using Oracle.ManagedDataAccess.Client;
    using Utilities;

    /// <summary>
    /// The user database service implementation.
    /// </summary>
    public class DbUserService : IDbUserService
    {
        /// <summary>
        /// The data access factory
        /// </summary>
        private readonly IDatabaseHelper databaseHelper;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DbUserService"/> class. 
        /// </summary>
        /// <param name="databaseHelper">The database helper.</param>
        public DbUserService(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        #endregion

        /// <summary>
        /// Gets the user from the database that matches the specified request. 
        /// </summary>
        /// <param name="request">The user request model.</param>
        /// <returns>Expected user model.</returns>
        public User GetUser(EUser request)
        {
            var user = new User();

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
                            const string StoredProcedureName = @"USP_USUARIO_SELECT";

                            // Create database command object.
                            using (var command = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureName, connection))
                            {
                                // Clear the parameters.
                                command.Parameters.Clear();

                                // Add the parameters to the list.
                                command.Parameters.Add(this.databaseHelper.CreateParameter("P_USUARIO", OracleDbType.Varchar2, request.UserName));
                                command.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                // Execute the reader.
                                using (var reader = command.ExecuteReader())
                                {
                                    // Read the data rows.
                                    if (reader.Read())
                                    {
                                        user = GetUserFromReader(reader);
                                    }
                                }
                            }

                            if (user.UserId > 0)
                            {
                                // The SQL stored procedure name.
                                const string StoredProcedureNameUP = @"USP_USUARIO_PERFIL_SELECT";

                                // Create database command object.
                                using (var commandUserProfile = this.databaseHelper.CreateStoredProcDbCommand(StoredProcedureNameUP, connection))
                                {
                                    var profiles = new List<UserProfile>();

                                    // Clear the parameters.
                                    commandUserProfile.Parameters.Clear();

                                    // Add the parameters to the list.
                                    commandUserProfile.Parameters.Add(this.databaseHelper.CreateParameter("P_ID_USUARIO", OracleDbType.Int32, user.UserId));
                                    commandUserProfile.Parameters.Add(this.databaseHelper.CreateParameter("refCursor", OracleDbType.RefCursor, 0, ParameterDirection.Output));

                                    // Execute the reader.
                                    using (var reader = commandUserProfile.ExecuteReader())
                                    {
                                        // Read the data rows.
                                        while (reader.Read())
                                        {
                                            var up = GetUserProfileFromReader(reader);
                                            profiles.Add(up);
                                        }
                                    }

                                    user.Profiles = profiles;
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

            return user;
        }

        #region Auxiliar Methods
        /// <summary>
        /// Gets the user from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected user model.</returns>
        private static User GetUserFromReader(IDataReader reader)
        {
            var user = new User();

            user.UserId = reader["ID_USUARIO"] is DBNull ? 0 : Convert.ToInt32(reader["ID_USUARIO"]);
            user.Usuario = reader["USERNAME"] is DBNull ? string.Empty : Convert.ToString(reader["USERNAME"]);
            user.Nombre = reader["USUARIO"] is DBNull ? string.Empty : Convert.ToString(reader["USUARIO"]);
            user.InstitucionId = reader["ID_INSTITUCION"] is DBNull ? 0 : Convert.ToInt32(reader["ID_INSTITUCION"]);

            return user;
        }

        /// <summary>
        /// Gets the user profile from the data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The expected user profile model.</returns>
        private static UserProfile GetUserProfileFromReader(IDataReader reader)
        {
            var userProfile = new UserProfile();

            userProfile.ProfileId = reader["ID_PERFIL"] is DBNull ? 0 : Convert.ToInt32(reader["ID_PERFIL"]);
            userProfile.Profile = reader["PERFIL"] is DBNull ? string.Empty : Convert.ToString(reader["PERFIL"]);
            userProfile.AreaId = reader["ID_AREA"] is DBNull ? 0 : Convert.ToInt32(reader["ID_AREA"]);
            userProfile.InstitucionId = reader["ID_INSTITUCION"] is DBNull ? 0 : Convert.ToInt32(reader["ID_INSTITUCION"]);

            return userProfile;
        }

        #endregion
    }
}
