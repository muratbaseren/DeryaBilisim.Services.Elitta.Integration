using RestSharp;
using System;
using System.ComponentModel.DataAnnotations;

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
        /// Elitta service object that connect to Elitta API Service
        /// </summary>
        /// <param name="endpoint">Elitta API root end point.</param>
        /// <param name="token">Elitta API Access Token.</param>
        public ElittaService(string endpoint, string token)
        {
            _client = new RestClient(endpoint);
            _client.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(token);
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
                return;
            }

            throw new UnauthorizedAccessException("Wrong username or password");
        }

        /// <summary>
        /// Authenticate to elitta service
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns></returns>
        private IRestResponse<ElittaServiceResponse<AuthenticateResponse>> Authenticate(AuthenticateModel model)
        {
            var request = new RestRequest("/Account/Authenticate", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<AuthenticateResponse>>(request);
        }

        /// <summary>
        /// Create individual account
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns></returns>
        private IRestResponse<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>> CreateIndividualAccount(IndividualAccountCreateModel model)
        {
            var request = new RestRequest("/Account/CreateIndividualAccount", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>>(request);
        }

    }

    /// <summary>
    /// Authenticate Model
    /// </summary>
    public partial class AuthenticateModel
    {
        /// <summary>
        /// Username that must be e-mail address
        /// </summary>
        [Required, StringLength(40), EmailAddress]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required, StringLength(16)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Individual Account Create Model
    /// </summary>
    public partial class IndividualAccountCreateModel
    {
        /// <summary>
        /// Account name
        /// </summary>
        [StringLength(25)]
        public string Name { get; set; }

        /// <summary>
        /// Account surname
        /// </summary>
        [StringLength(25)]
        public string Surname { get; set; }

        /// <summary>
        /// Account E-Mail(username)
        /// </summary>
        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Account phone that must be 10-digital character. For example; 05556667788
        /// </summary>
        [Required, RegularExpression(@"^0\d{10}$", ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz. Örnek format:05556667788")]
        public string Phone { get; set; }

        /// <summary>
        /// Account Address
        /// </summary>
        [StringLength(250)]
        public string Address { get; set; }
    }














    /// <summary>
    /// Authenticate Response
    /// </summary>
    public partial class AuthenticateResponse
    {
        /// <summary>
        /// Access token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Access token expire datetime
        /// </summary>
        public DateTime AccessTokenExpire { get; set; }
        /// <summary>
        /// Account unique code
        /// </summary>
        public Guid UniqueCode { get; set; }
    }

    /// <summary>
    /// Individual Account Unique Code Response
    /// </summary>
    public partial class IndividualAccountUniqueCodeResponse
    {
        /// <summary>
        /// Account e-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Account phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Account unique code
        /// </summary>
        public Guid UniqueCode { get; set; }
    }
}
