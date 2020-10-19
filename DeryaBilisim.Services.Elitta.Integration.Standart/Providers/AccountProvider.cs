using RestSharp;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
    /// <summary>
    /// Account provider for account methods
    /// </summary>
    public partial class AccountProvider
    {
        private readonly IRestClient _client;

        /// <summary>
        /// Create account provider
        /// </summary>
        /// <param name="client">Rest client object</param>
        public AccountProvider(IRestClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Create individual account
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>> CreateIndividualAccount(IndividualAccountCreateModel model)
        {
            var request = new RestRequest("/Account/CreateIndividualAccount", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>>(request);
        }
    }
}
