//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Core.Resources
{
    /// <summary>
    /// The CONVENIO Constants definition.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// The CONVENIO API resource (WEB API port).
        /// </summary>
        public const string ConvenioApi = "https://localhost:44326/";

        /// <summary>
        /// The CONVENIO angular resource (ANGULAR CLIENT port).
        /// </summary>
        public const string ConvenioAngular = "https://localhost:44334/";

        /// <summary>
        /// The CONVENIO client secret.
        /// </summary>
        public const string ConvenioClientSecret = "myrandomclientsecret";

        /// <summary>
        /// The CONVENIO issue URI (IDENTITY).
        /// </summary>
        public const string ConvenioIssuerUri = "https://localhost:44391/identity";////"https://tripcompanysts/identity";

        /// <summary>
        /// The CONVENIO STS origin (IDENTITY SERVER).
        /// </summary>
        public const string ConvenioStsOrigin = "https://localhost:44391/";

        /// <summary>
        /// The CONVENIO STS.
        /// </summary>
        public const string ConvenioSts = ConvenioStsOrigin + "/identity";

        /*public const string TripGalleryAPI = "https://localhost:44315/"; // https web api 
        public const string TripGalleryAngular = "https://localhost:44316/"; // https angular
        public const string TripGalleryIssuerUri = "https://tripcompanysts/identity";
        public const string TripGallerySTSOrigin = "https://localhost:44317"; //// https identity server 
        public const string TripGallerySTS = TripGallerySTSOrigin + "/identity";
        public const string TripGallerySTSTokenEndpoint = TripGallerySTS + "/connect/token";
        public const string TripGallerySTSAuthorizationEndpoint = TripGallerySTS + "/connect/authorize";
        public const string TripGallerySTSUserInfoEndpoint = TripGallerySTS + "/connect/userinfo";
        public const string TripGallerySTSEndSessionEndpoint = TripGallerySTS + "/connect/endsession";
        public const string TripGallerySTSRevokeTokenEndpoint = TripGallerySTS + "/connect/revocation";*/

        ////public const string TripGallerySTSTokenEndpoint = TripGallerySTS + "/connect/token";
        ////public const string TripGallerySTSAuthorizationEndpoint = TripGallerySTS + "/connect/authorize";
        ////public const string TripGallerySTSUserInfoEndpoint = TripGallerySTS + "/connect/userinfo";
        ////public const string TripGallerySTSEndSessionEndpoint = TripGallerySTS + "/connect/endsession";
        ////public const string TripGallerySTSRevokeTokenEndpoint = TripGallerySTS + "/connect/revocation";
    }
}
