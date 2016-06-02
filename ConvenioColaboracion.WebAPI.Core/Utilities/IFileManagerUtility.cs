//-----------------------------------------------------------------------
// <copyright file="IFileManagerUtility.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Core.Utilities
{
    /// <summary>
    /// The File Manager Utility Interface.
    /// </summary>
    public interface IFileManagerUtility
    {
        /// <summary>
        /// Gets the document path.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="destinationFolder">The destination folder name.</param>
        /// <returns>The generated document path.</returns>
        string GetDocumentPath(string fileName, string destinationFolder);

        /// <summary>
        /// Copies the specified document to the destination path.
        /// </summary>
        /// <param name="document">The document string representation.</param>
        /// <param name="finalPath">The destiny file path.</param>
        /// <returns>A value indicating whether the file was copied or not.</returns>
        bool CopyDocument(string document, string finalPath);
    }
}
