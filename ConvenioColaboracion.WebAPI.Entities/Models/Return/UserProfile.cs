//-----------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Entities.Models.Return
{
    /// <summary>
    /// The user profile return model.
    /// </summary>  
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the user's profile identifier.
        /// </summary>
        /// <value>The user's profile identifier.</value>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the institution identifier.
        /// </summary>
        /// <value>The user institution.</value>
        public int InstitucionId { get; set; }

        /// <summary>
        /// Gets or sets the area identifier.
        /// </summary>
        /// <value>The area identifier.</value>
        public int AreaId { get; set; }
    }
}