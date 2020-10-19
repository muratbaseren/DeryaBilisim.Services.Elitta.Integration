using System.Collections.Generic;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
    /// <summary>
    /// Elitta API service response object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class ElittaServiceResponse<T>
    {
        /// <summary>
        /// Indicates response has error.
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Indicates response error messages.
        /// </summary>
        public List<string> ErrorMessages { get; set; } = new List<string>();

        /// <summary>
        /// Indicates response data.
        /// </summary>
        public T Data { get; set; }
    }
}
