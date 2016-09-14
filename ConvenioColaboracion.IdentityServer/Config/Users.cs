//-----------------------------------------------------------------------
// <copyright file="Users.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.IdentityServer.Config
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using IdentityServer3.Core;
    using IdentityServer3.Core.Services.InMemory;

    /// <summary>
    /// The identity server users implementation class.
    /// </summary>
    public class Users
    {
        /// <summary>
        /// Gets all the users.
        /// </summary>
        /// <returns>The expected list of users.</returns>
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>()
            {
                 new InMemoryUser
                {
                    Username = "Kevin",
                    Password = "secret",
                    Subject = "b05d3546-6ca8-4d32-b95c-77e94d705ddf",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Kevin"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Dockx"),
                        new Claim(Constants.ClaimTypes.Address, "1, Main Street, Antwerp, Belgium")
                    }
                 }
                ,
                new InMemoryUser
                {
                    Username = "Sven",
                    Password = "secret",
                    Subject = "bb61e881-3a49-42a7-8b62-c13dbe102018",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Sven"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Vercauteren"),
                        new Claim(Constants.ClaimTypes.Address, "2, Main Road, Antwerp, Belgium")
                    }
                }
            };
        }

        //// TODO: Change this method to get all the users from the database
        ////public static List<User>
    }
}