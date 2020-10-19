using RestSharp;
using System;
using System.Collections.Generic;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
    /// <summary>
    /// AppAccount provider for app account methods
    /// </summary>
    public partial class AppAccountProvider
    {
        private readonly IRestClient _client;

        /// <summary>
        /// Create account provider
        /// </summary>
        /// <param name="client">Rest client object</param>
        public AppAccountProvider(IRestClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Get company list.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<AppAccountCompanyUniqueCodeResponse>> GetCompanies()
        {
            var request = new RestRequest("/AppAccount/Companies", Method.GET, DataFormat.Json);
            return _client.Get<ElittaServiceResponse<AppAccountCompanyUniqueCodeResponse>>(request);
        }

        /// <summary>
        /// Get individual account by phone number
        /// </summary>
        /// <param name="phone">Account phone that must be 11-digital characters and start with zero. For example; 05556667788</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>> GetByPhone(string phone)
        {
            var request = new RestRequest("/AppAccount/GetByPhone/{phone}", Method.GET, DataFormat.Json)
                .AddUrlSegment("phone", phone);
            return _client.Get<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>>(request);
        }

        /// <summary>
        /// Get individual account by e-mail address
        /// </summary>
        /// <param name="email">Account e-mail address that must be an e-mail address format.</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>> GetByEmail(string email)
        {
            var request = new RestRequest("/AppAccount/GetByEmail/{email}", Method.GET, DataFormat.Json)
                .AddUrlSegment("email", email);
            return _client.Get<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>>(request);
        }

        /// <summary>
        /// Get individual account by phone or e-mail address. Phone number first searching.
        /// </summary>
        /// <param name="model">Account e-mail address that must be an e-mail address format or phone number that must be 11-digital characters and start with zero. For example; 05556667788</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>> GetByPhoneOrEmail(GetByPhoneOrEmailModel model)
        {
            var request = new RestRequest("/AppAccount/GetByPhoneOrEmail", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<IndividualAccountUniqueCodeResponse>>(request);
        }

        /// <summary>
        /// Get company totals by individual unique code
        /// </summary>
        /// <param name="uniqueCode">Individual unique code</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<List<OperationTotalByCompanyResponse>>> GetCompanyTotalsByUniqueCode(Guid uniqueCode)
        {
            var request = new RestRequest("/AppAccount/GetCompanyTotalsByUniqueCode/{uniqueCode}", Method.GET, DataFormat.Json)
                .AddUrlSegment("uniqueCode", uniqueCode.ToString());

            return _client.Get<ElittaServiceResponse<List<OperationTotalByCompanyResponse>>>(request);
        }

        /// <summary>
        /// Win elitta for individual account to company account
        /// </summary>
        /// <param name="model">Options</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<string>> WinElitta(WinElittaModel model)
        {
            var request = new RestRequest("/AppAccount/WinElitta", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<string>>(request);
        }

        /// <summary>
        /// Spend elitta for company account to individual account
        /// </summary>
        /// <param name="model">Options</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<string>> SpendElitta(SpendElittaModel model)
        {
            var request = new RestRequest("/AppAccount/SpendElitta", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<string>>(request);
        }

        /// <summary>
        /// Spend elitta for company account to individual account
        /// </summary>
        /// <param name="model">Options</param>
        /// <returns></returns>
        public IRestResponse<ElittaServiceResponse<string>> RefundElitta(RefundElittaModel model)
        {
            var request = new RestRequest("/AppAccount/RefundElitta", Method.POST, DataFormat.Json);
            request.AddJsonBody(model);
            return _client.Post<ElittaServiceResponse<string>>(request);
        }
    }
}
