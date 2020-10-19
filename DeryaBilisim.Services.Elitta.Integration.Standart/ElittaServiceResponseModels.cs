using System;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
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

    /// <summary>
    /// App Account Company Unique Code Response
    /// </summary>
    public partial class AppAccountCompanyUniqueCodeResponse
    {
        /// <summary>
        /// Company name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Company commercial title
        /// </summary>
        public string CommercialTitle { get; set; }

        /// <summary>
        /// Company unique code
        /// </summary>
        public Guid UniqueCode { get; set; }
    }

    /// <summary>
    /// Operation Total By Company Response
    /// </summary>
    public partial class OperationTotalByCompanyResponse
    {
        /// <summary>
        /// Company unique code
        /// </summary>
        public Guid CompanyUniqueCode { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Total elitta for company
        /// </summary>
        public decimal TotalElitta { get; set; }
    }
}
