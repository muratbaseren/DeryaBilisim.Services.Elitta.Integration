using Microsoft.Extensions.DependencyInjection;

namespace DeryaBilisim.Services.Elitta.Integration.Standart
{
    /// <summary>
    /// Elitta Service Extensions
    /// </summary>
    public static class ElittaServiceExtensions
    {
        /// <summary>
        /// Add Elitta Service to services by singleton.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="endpoint">Elitta API service end point.</param>
        /// <param name="token">Elitta API service token.</param>
        public static void AddElittaService(this IServiceCollection services, string endpoint, string token)
        {
            services.AddSingleton<ElittaService>(x => new ElittaService(endpoint, token));
        }

        /// <summary>
        /// Add Elitta Service to services by singleton.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="endpoint">Elitta API service end point.</param>
        /// <param name="username">Elitta API service username.</param>
        /// <param name="password">Elitta API service password.</param>
        public static void AddElittaService(this IServiceCollection services, string endpoint, string username, string password)
        {
            services.AddSingleton<ElittaService>(x => new ElittaService(endpoint, username, password));
        }
    }
}
