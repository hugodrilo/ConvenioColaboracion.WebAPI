//-----------------------------------------------------------------------
// <copyright file="WinAuthController.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Controllers
{
    using System.Diagnostics;
    using System.Web.Http;
    using ConvenioColaboracion.WebAPI.Core.Logging;

    /// <summary>
    /// The Windows Authentication controller implementation class.
    /// </summary>
    public class WinAuthController : ApiController
    {
        /// <summary>
        /// Gets or sets the Logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Checks if the user is Authenticated.
        /// </summary>
        /// <returns>A value indicating whether the user is authenticated or not.</returns>
        [HttpGet]
        [Route("api/testauthentication")]
        public IHttpActionResult TestAutentication()
        {
            Debug.Write("AuthenticationType:" + User.Identity.AuthenticationType);
            Debug.Write("IsAuthenticated:" + User.Identity.IsAuthenticated);
            Debug.Write("Name:" + User.Identity.Name);

            if (User.Identity.IsAuthenticated)
            {
                this.Logger.Info("User: " + User.Identity.Name);

                return this.Ok("Authenticated: " + User.Identity.Name);
            }

            return this.BadRequest("Not authenticated");
        }
    }
}
