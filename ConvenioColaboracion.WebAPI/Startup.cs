//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI
{
    using System.Web.Http;
    using IdentityServer3.AccessTokenValidation;
    using Owin;

    /// <summary>
    /// The Web API startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes configuration values.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(
                 new IdentityServerBearerTokenAuthenticationOptions
                 {
                     Authority = @"https://localhost:44391/identity",
                     RequiredScopes = new[] { "convenioColaboracion" }
                 });

            // configure Web Api
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Manually assign httpConfig from GlobalConfiguration
            var httpConfig = GlobalConfiguration.Configuration;

            // Use same config with OWIN app
            app.UseWebApi(httpConfig);

            InitAutoMapper();
        }

        /// <summary>
        /// Initializes the auto mapper.
        /// </summary>
        private static void InitAutoMapper()
        {
            ////   Mapper.CreateMap<Repository.Entities.Trip,
            ////       DTO.Trip>().ForMember(dest => dest.MainPictureUri,
            ////       op => op.ResolveUsing(typeof(InjectImageBaseForTripResolver)));

            ////   Mapper.CreateMap<Repository.Entities.Picture,
            ////       DTO.Picture>()
            ////       .ForMember(dest => dest.Uri,
            ////       op => op.ResolveUsing(typeof(InjectImageBaseForPictureResolver)));

            ////   Mapper.CreateMap<DTO.Picture,
            ////     Repository.Entities.Picture>();        

            ////   Mapper.CreateMap<DTO.Trip,
            ////       Repository.Entities.Trip>().ForMember(dest => dest.MainPictureUri,
            ////       op => op.ResolveUsing(typeof(RemoveImageBaseForTripResolver))); ;

            ////   Mapper.CreateMap<DTO.PictureForCreation,
            ////       Repository.Entities.Picture>()
            ////       .ForMember(o => o.Id, o => o.Ignore())
            ////       .ForMember(o => o.TripId, o => o.Ignore())
            ////       .ForMember(o => o.OwnerId, o => o.Ignore())
            ////       .ForMember(o => o.Uri, o => o.Ignore());

            ////   Mapper.CreateMap<DTO.TripForCreation,
            ////Repository.Entities.Trip>()
            ////   .ForMember(o => o.Id, o => o.Ignore())
            ////   .ForMember(o => o.MainPictureUri, o => o.Ignore())
            ////   .ForMember(o => o.Pictures, o => o.Ignore())
            ////   .ForMember(o => o.OwnerId, o => o.Ignore());

            ////   Mapper.AssertConfigurationIsValid();
        }
    }
}