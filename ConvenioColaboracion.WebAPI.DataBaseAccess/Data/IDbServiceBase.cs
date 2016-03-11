//-----------------------------------------------------------------------
// <copyright file="IDbServiceBase.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Data
{
    /// <summary>
    /// The database service base interface.
    /// </summary>
    /// <typeparam name="T">The generic type parameter.</typeparam>
    public interface IDbServiceBase<T>
    {
        /// <summary>
        /// Insert an area in the database.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>A value indicating whether the area was inserted or not.</returns>
        bool Insert(T request);

        /// <summary>
        /// Update the area in the database.
        /// </summary>
        /// <param name="request">The  request model.</param>
        /// <returns>A value indicating whether the area was updated or not.</returns>
        bool Update(T request);

        /// <summary>
        /// Delete an area in the database.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>A value indicating whether the area was deleted or not.</returns>
        bool Delete(T request);
    }
}
