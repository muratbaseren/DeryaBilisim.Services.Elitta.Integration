using RestSharp;
using System;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
    /// <summary>
    /// Elitta service
    /// </summary>
    public partial class ElittaService
    {
        private readonly IRestClient _client;

        /// <summary>
        /// Indicates authentication info for username and password instance creation
        /// </summary>
        public AuthenticateResponse AuthenticateInfo { get; protected set; }

        /// <summary>
        /// Account provider for account operations
        /// </summary>
        public AccountProvider AccountProvider { get; protected set; }

        /// <summary>
        /// App Account provider for app account operations
        /// </summary>
        public AppAccountProvider AppAccountProvider { get; protected set; }

        /// <summary>
        /// Contact message provider
        /// </summary>
        public ContactMessageProvider ContactMessageProvider { get; protected set; }

        /// <summary>
        /// Offer message provider
        /// </summary>
        public OfferMessageProvider OfferMessageProvider { get; protected set; }

        /// <summary>
        /// Elitta service object that connect to Elitta API Service
        /// </summary>
        /// <param name="endpoint">Elitta API root end point.</param>
        /// <param name="token">Elitta API Access Token.</param>
        public ElittaService(string endpoint, string token)
        {
            _client = new RestClient(endpoint);
            _client.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(token);
         
            CreateProviders();
        }

        /// <summary>
        /// Elitta service object that connect to Elitta API Service
        /// </summary>
        /// <param name="endpoint">Elitta API root end point.</param>
        /// <param name="username">Elitta API username.</param>
        /// <param name="password">Elitta API password.</param>
        public ElittaService(string endpoint, string username, string password)
        {
            _client = new RestClient(endpoint);

            var response = Authenticate(new AuthenticateModel { Username = username, Password = password });

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var token = response?.Data?.Data?.AccessToken ?? throw new UnauthorizedAccessException("Wrong username or password");
                _client.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(token);

                AuthenticateInfo = response?.Data?.Data;

                CreateProviders();
                return;
            }

            throw new UnauthorizedAccessException("Wrong username or password");
        }

        
        private IRestResponse<ElittaServiceResponse<AuthenticateResponse>> Authenticate(AuthenticateModel model)
        {
            var request = new RestRequest("/Account/Authenticate", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<AuthenticateResponse>>(request);
        }

        private void CreateProviders()
        {
            AccountProvider = new AccountProvider(_client);
            AppAccountProvider = new AppAccountProvider(_client);
            ContactMessageProvider = new ContactMessageProvider(_client);
            OfferMessageProvider = new OfferMessageProvider(_client);
        }
    }
}
