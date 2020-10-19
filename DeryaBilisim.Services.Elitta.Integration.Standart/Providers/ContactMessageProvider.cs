using RestSharp;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
    /// <summary>
    /// Contact Message provider
    /// </summary>
    public partial class ContactMessageProvider
    {
        private readonly IRestClient _client;

        /// <summary>
        /// Create contact Message provider
        /// </summary>
        /// <param name="client">Rest client object</param>
        public ContactMessageProvider(IRestClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Contact message save
        /// </summary>
        /// <param name="model">Message options</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<string>> Save(ContactMessageCreateModel model)
        {
            var request = new RestRequest("/ContactMessage/Save", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<string>>(request);
        }
    }
}
