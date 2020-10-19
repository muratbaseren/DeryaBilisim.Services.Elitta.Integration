using RestSharp;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
    /// <summary>
    /// Elitta service object
    /// </summary>
    public partial class ElittaService
    {
        private readonly IRestClient _client;

        /// <summary>
        /// Elitta service object that connect to Elitta API Service
        /// </summary>
        /// <param name="endpoint">Elitta API root end point.</param>
        /// <param name="token">Elitta API Access Token.</param>
        public ElittaService(string endpoint, string token)
        {
            _client = new RestClient(endpoint);
            _client.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(token);
        }
    }
}
