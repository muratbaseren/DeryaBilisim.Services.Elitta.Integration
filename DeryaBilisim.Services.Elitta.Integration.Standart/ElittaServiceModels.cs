using System;
using System.ComponentModel.DataAnnotations;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
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
        /// Account phone that must be 11-digital characters and start with zero. For example; 05556667788
        /// </summary>
        [Required, RegularExpression(@"^0\d{10}$", ErrorMessage = "Account phone that must be 11-digital characters and start with zero. For example; 05556667788")]
        public string Phone { get; set; }

        /// <summary>
        /// Account Address
        /// </summary>
        [StringLength(250)]
        public string Address { get; set; }
    }

    /// <summary>
    /// Get By Phone Or Email Model
    /// </summary>
    public partial class GetByPhoneOrEmailModel
    {
        /// <summary>
        /// Phone that must be 11-digital characters and start with zero. For example; 05556667788
        /// </summary>
        [RegularExpression(@"^0\d{10}$", ErrorMessage = "Account phone that must be 11-digital characters and start with zero. For example; 05556667788")]
        public string Phone { get; set; }

        /// <summary>
        /// E-mail address
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
    }

    /// <summary>
    /// Win Elitta Model
    /// </summary>
    public partial class WinElittaModel
    {
        /// <summary>
        /// Elitta activate datetime (optional)
        /// </summary>
        public DateTime? ActivateDate { get; set; }

        /// <summary>
        /// Elitta activity end datetime (optional)
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Campaign number (optional)
        /// </summary>
        [StringLength(40)]
        public string CampaignNo { get; set; }

        /// <summary>
        /// Order number (optional)
        /// </summary>
        [StringLength(40)]
        public string OrderNo { get; set; }

        /// <summary>
        /// Elitta amount
        /// </summary>
        public decimal Elitta { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [StringLength(250)]
        public string Description { get; set; }

        /// <summary>
        /// Individual unique code that using to transfering elitta company account to individual account (required)
        /// </summary>
        [Required]
        public Guid IndividualUniqueCode { get; set; }

        /// <summary>
        /// Company unique code that using to transfering elitta company account to individual account (required)
        /// </summary>
        [Required]
        public Guid CompanyUniqueCode { get; set; }
    }

    /// <summary>
    /// Spend Elitta Model
    /// </summary>
    public partial class SpendElittaModel
    {
        /// <summary>
        /// Campaign number (optional)
        /// </summary>
        [StringLength(40)]
        public string CampaignNo { get; set; }

        /// <summary>
        /// Order number (optional)
        /// </summary>
        [StringLength(40)]
        public string OrderNo { get; set; }

        /// <summary>
        /// Elitta amount
        /// </summary>
        public decimal Elitta { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [StringLength(250)]
        public string Description { get; set; }

        /// <summary>
        /// Individual unique code that using to transfering elitta individual account to company account (required)
        /// </summary>
        [Required]
        public Guid IndividualUniqueCode { get; set; }

        /// <summary>
        /// Company unique code that using to transfering elitta individual account to company account (required)
        /// </summary>
        [Required]
        public Guid CompanyUniqueCode { get; set; }
    }

    /// <summary>
    /// Refund Elitta Model
    /// </summary>
    public partial class RefundElittaModel
    {
        /// <summary>
        /// Campaign number (optional)
        /// </summary>
        [StringLength(40)]
        public string CampaignNo { get; set; }

        /// <summary>
        /// Order number (optional)
        /// </summary>
        [StringLength(40)]
        public string OrderNo { get; set; }

        /// <summary>
        /// Elitta amount
        /// </summary>
        public decimal Elitta { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [StringLength(250)]
        public string Description { get; set; }

        /// <summary>
        /// Individual unique code that using to transfering elitta individual account to company account (required)
        /// </summary>
        [Required]
        public Guid IndividualUniqueCode { get; set; }

        /// <summary>
        /// Company unique code that using to transfering elitta individual account to company account (required)
        /// </summary>
        [Required]
        public Guid CompanyUniqueCode { get; set; }
    }

    /// <summary>
    /// Contact Message Create Model
    /// </summary>
    public class ContactMessageCreateModel
    {
        /// <summary>
        /// First name
        /// </summary>
        [Required, MaxLength(30)]
        public string Firstname { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Required, MaxLength(30)]
        public string Lastname { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [Required, MaxLength(15)]
        public string Phone { get; set; }

        /// <summary>
        /// E-mail address
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [Required, MaxLength(500)]
        public string Message { get; set; }
    }

    /// <summary>
    /// Offer Message Create Model
    /// </summary>
    public class OfferMessageCreateModel
    {
        /// <summary>
        /// Company name
        /// </summary>
        [Required, MaxLength(50)]
        public string CompanyName { get; set; }

        /// <summary>
        /// Company commercial title
        /// </summary>
        [Required, MaxLength(100)]
        public string CompanyCommercialTitle { get; set; }

        /// <summary>
        /// Company tax number
        /// </summary>
        [Required, MaxLength(30)]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Company phone number
        /// </summary>
        [Required, MaxLength(15)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Company address
        /// </summary>
        [Required, MaxLength(300)]
        public string Address { get; set; }

        /// <summary>
        /// Company e-mail address
        /// </summary>
        [Required, EmailAddress]
        public string CompanyEmail { get; set; }

        /// <summary>
        /// Company competent first name
        /// </summary>
        [Required, MaxLength(30)]
        public string CompetentFirstname { get; set; }

        /// <summary>
        /// Company competent last name
        /// </summary>
        [Required, MaxLength(30)]
        public string CompetentLastname { get; set; }

        /// <summary>
        /// Company competent phone number
        /// </summary>
        [Required, MaxLength(15)]
        public string CompetentPhoneNumber { get; set; }

        /// <summary>
        /// Company competent e-mail address
        /// </summary>
        [Required, EmailAddress]
        public string CompetentEmail { get; set; }
    }
}
