using AspNetCoreSample.Models;
using DeryaBilisim.Services.Elitta.Integration.Standart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AspNetCoreSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ElittaService _elittaService;

        public HomeController(ILogger<HomeController> logger, ElittaService elittaService)
        {
            _logger = logger;
            _elittaService = elittaService;
        }

        public IActionResult Index()
        {
            var response = _elittaService.AccountProvider.CreateIndividualAccount(new IndividualAccountCreateModel { Email = "shv89744@bcaoo.com", Phone = "99996282846", Name = "name1", Surname = "surname1", Address = "address1" });

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
