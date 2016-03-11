//-----------------------------------------------------------------------
// <copyright file="ConvenioFixture.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Test.Fixtures
{
    using System;
    using ConvenioColaboracion.WebAPI.DataBaseAccess.Utilities;
    using Data;

    /// <summary>
    /// The CONVENIO fixture service tests.
    /// </summary>
    public class ConvenioFixture : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvenioFixture"/> class. 
        /// </summary>
        public ConvenioFixture()
        {
            this.DatabaseHelper = new DatabaseHelper();
            this.Sut = new DbConvenioService(this.DatabaseHelper);
        }

        /// <summary>
        /// Gets the system under test.
        /// </summary>
        public DbConvenioService Sut { get; private set; }

        /// <summary>
        /// Gets or sets the database helper.
        /// </summary>
        public DatabaseHelper DatabaseHelper { get; set; }

        /// <summary>
        /// The dispose method.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
