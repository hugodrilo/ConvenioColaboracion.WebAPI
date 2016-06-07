//-----------------------------------------------------------------------
// <copyright file="FileManagerUtility.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Core.Utilities
{
    using System;
    using System.IO;

    /// <summary>
    /// The File Manager Utility Implementation
    /// </summary>
    public class FileManagerUtility : IFileManagerUtility
    {
        /// <summary>
        /// Gets the document path.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="destinationFolder">The destination folder name.</param>
        /// <returns>The generated document path.</returns>
        public string GetDocumentPath(string fileName, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            var destinyFilePath = destinationFolder + @"\" + fileName;

            return destinyFilePath;
        }

        /// <summary>
        /// Copies the CONVENIO document to the destiny path.
        /// </summary>
        /// <param name="document">The document string representation.</param>
        /// <param name="finalPath">The destiny file path.</param>
        /// <returns>A value indicating whether the file was copied or not.</returns>
        public bool CopyDocument(string document, string finalPath)
        {
            var fileCopied = false;

            try
            {
                var myString = document.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var bytes = Convert.FromBase64String(myString[1]);

                using (var ms = new MemoryStream(bytes))
                {
                    using (var file = new FileStream(finalPath, FileMode.Create, FileAccess.Write))
                    {
                        if (file.CanWrite)
                        {
                            // Write to file
                            ms.WriteTo(file);
                            fileCopied = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return fileCopied;
        }
    }
}
