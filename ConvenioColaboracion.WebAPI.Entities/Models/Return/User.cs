//-----------------------------------------------------------------------
// <copyright file="User.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Return
{
    /// <summary>
    /// The user return model.
    /// </summary>  
    public class User
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's active directory GUID.
        /// </summary>
        /// <value>The user's active directory GUID.</value>
        public string Usuario { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The user display name.</value>
        public string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the institution identifier.
        /// </summary>
        /// <value>The user institution.</value>
        public int InstitucionId { get; set; }
    }
}