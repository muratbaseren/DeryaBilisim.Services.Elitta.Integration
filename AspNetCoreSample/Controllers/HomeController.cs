using AspNetCoreSample.Models;
using DeryaBilisim.Services.Elitta.Integration.Standart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

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
            string email = "sjc88323@eoopy.com";

            // Get individual account by e-mail address..
            var individualAccount = _elittaService.AppAccountProvider.GetByEmail(email)?.Data?.Data ?? null;

            if (individualAccount == null)
            {
                // Create individual account..
                var individualAccountResponse = _elittaService.AccountProvider.CreateIndividualAccount(
                    new IndividualAccountCreateModel
                    {
                        Email = email,
                        Phone = "05556282846",
                        Name = "john",
                        Surname = "doe",
                        Address = "Lorem ipsum dolor sit a met."
                    });

                if (individualAccountResponse.IsSuccessful)
                {
                    if (individualAccountResponse?.Data.Error == false)
                    {
                        individualAccount = individualAccountResponse?.Data?.Data;
                    }
                    else
                    {
                        // If operation get error..
                        ViewBag.Result = string.Join(" | ", individualAccountResponse?.Data.ErrorMessages);
                        return View();
                    }
                }
                else
                {
                    ViewBag.Result = "Request failed.";
                    return View();
                }
            }




            // Get all companies for app account that must be assign to companies on elitta panel.
            var companies = _elittaService.AppAccountProvider.GetCompanies()?.Data?.Data;

            // Set elitta to individual account from company account
            var winElittaResponse = _elittaService.AppAccountProvider.WinElitta(new WinElittaModel
            {
                CompanyUniqueCode = companies.First().UniqueCode,
                IndividualUniqueCode = individualAccount.UniqueCode,
                OrderNo = "ORD-456678",
                CampaignNo = "CAM-123",
                Elitta = 10,
                Description = "Shopping elitta transfer"
            });

            if (winElittaResponse.IsSuccessful)
            {
                // Set elitta is successfully.
                if (winElittaResponse.Data.Error == false && winElittaResponse.Data.Data == "ok")
                {
                    ViewBag.Result = $"Set <b>{10} elitta</b> from '{companies.First().CompanyName}' to '{individualAccount.Email}'";
                }
                else
                {
                    // If operation get error..
                    ViewBag.Result = string.Join(" | ", winElittaResponse?.Data.ErrorMessages);
                }
            }
            else
            {
                ViewBag.Result = "Request failed.";
            }



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
