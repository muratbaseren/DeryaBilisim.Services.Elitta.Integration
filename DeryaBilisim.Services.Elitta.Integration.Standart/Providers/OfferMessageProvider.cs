using RestSharp;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
    /// <summary>
    /// Offer Message provider
    /// </summary>
    public partial class OfferMessageProvider
    {
        private readonly IRestClient _client;

        /// <summary>
        /// Create offer Message provider
        /// </summary>
        /// <param name="client">Rest client object</param>
        public OfferMessageProvider(IRestClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Contact message save
        /// </summary>
        /// <param name="model">Message options</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<string>> Save(OfferMessageCreateModel model)
        {
            var request = new RestRequest("/OfferMessage/Save", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<string>>(request);
        }
    }
}
