//-----------------------------------------------------------------------
// <copyright file="UsersController.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Controllers
{
    using System.Security.Principal;
    using System.Web.Http;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Data;
    using ConvenioColaboracion.WebAPI.Entities.Models.Request;
    using ConvenioColaboracion.WebAPI.Entities.Models.Return;

    /// <summary>
    /// The Users controller implementation class.
    /// </summary>

    public class UsersController : ApiController
    {
        /// <summary>
        /// Gets or sets the database user service.
        /// </summary>
        public IDbUserService DbUserService { get; set; }

        /// <summary>
        ///  Gets the currently logged in user account in AD as a User object.
        /// </summary>
        /// <returns>Expected user model.</returns>
        [HttpGet]
        public User GetCurrentUser()
        {
            var user = new User();
            var request = new EUser();

            var windowsIdentity = WindowsIdentity.GetCurrent();

            if (windowsIdentity != null)
            {
                request.UserName = windowsIdentity.Name.Replace("SFP\\", string.Empty).ToUpper();
            }

            // The database call.
            var userInDatabase = this.DbUserService.GetUser(request);

            if (userInDatabase.UserId > 0)
            {
                user = userInDatabase;
            }

            return user;
        }
    }
}
