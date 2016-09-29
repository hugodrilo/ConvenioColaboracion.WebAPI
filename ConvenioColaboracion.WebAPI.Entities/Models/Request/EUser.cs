//-----------------------------------------------------------------------
// <copyright file="EUser.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Request
{
    /// <summary>
    /// The user request model.
    /// </summary>
    public class EUser
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The user display name.</value>
        public string UserName { get; set; }
    }
}
