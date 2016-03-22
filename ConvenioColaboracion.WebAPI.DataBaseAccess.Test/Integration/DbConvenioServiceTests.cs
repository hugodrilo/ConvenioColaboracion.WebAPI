//-----------------------------------------------------------------------
// <copyright file="DbConvenioServiceTests.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.DataBaseAccess.Test.Integration
{
    using ConvenioColaboracion.WebAPI.Entities.Models.Request;
    using Fixtures;
    using Xunit;

    /// <summary>
    /// Tests that verify the database CONVENIO service class.
    /// </summary>
    public class DbConvenioServiceTests : IClassFixture<ConvenioFixture>
    {
        /// <summary>
        /// The CONVENIO fixture field.
        /// </summary>
        private readonly ConvenioFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConvenioServiceTests"/> class. 
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public DbConvenioServiceTests(ConvenioFixture fixture)
        {
            this.fixture = fixture;
        }

        /// <summary>
        /// Test the GetAll method for success
        /// </summary>
        [Fact]
        public void DbConvenioService_INTG_GetAll_Success()
        {
            // The system under test.
            var sut = this.fixture.Sut;

            // The method to test.
            var result = sut.GetAll();

            // Asserts
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test the Post method for success
        /// </summary>
        [Fact]
        public void DbConvenioService_INTG_Post_Success()
        {
            // The system under test.
            var sut = this.fixture.Sut;

            var convenio = new EConvenio();

            // The method to test.
            var result = sut.Insert(convenio);

            // Asserts
            Assert.NotNull(result);
        }

        /// <summary>
        /// The dispose method.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
