# DeryaBilisim.Services.Elitta.Integration
Bielitta API servisi ile ileti�im kuracak olan nuget pack

## Gereksinimler
API sistemi, bielitta sistemindeki uygulama hesaplar� i�indir. Entegre olan uygulamalar�n m��terilerine puan kazan�m�, puan harcama y�netimi ve benzeri �zellikleri kazand�r�r. Uygulama hesab� kullan�c� ad� ve �ifreniz ile sisteminizin API ile konu�abilmesi i�in gerekli **Eri�im Anahtar Kodunu** elde edebilirsiniz. Bu eri�im anahtar� ile [nuget paketini](https://www.nuget.org/packages/DeryaBilisim.Services.Elitta.Integration.Standart) projenize ekleyerek entegre olabilirsiniz.


## Endpoints

Endpoint : https://bielitta.azurewebsites.net/api


## .NET Core MVC/API App Entegrasyonu

[DeryaBilisim.Services.Elitta.Integration.Standart](https://www.nuget.org/packages/DeryaBilisim.Services.Elitta.Integration.Standart) nuget paketini uygulaman�za ekleyiniz. 

### Kullan�m

#### **appsettings.json**
appsettings.json i�erisinde a�a��daki section � a�arak gerekli endpoint ve uygulama hesap bilgilerinizi giriniz.

```javascript
{
  "BielittaIntegration": {
    "Endpoint": "https://bielitta.azurewebsites.net/api",
    "Username": "app@myapplication.com",
    "Password": "123456"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

#### **Startup.cs**
Startup dosyas�nda gerekli servis entegrasyonunu sa�lay�n�z. appsettings.json de�erlerini kullan�n�z. Bielitta Servisi singleton olarak �retilecek �ekilde tan�mlanacakt�r. Bielitta servis i�erisinde yer alan provider lar� kullanarak istedi�iniz i�lemi ger�ekle�tirebilirsiniz.

```csharp
using DeryaBilisim.Services.Elitta.Integration.Standart;

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    var opts = Configuration.GetSection("BielittaIntegration").Get<BielittaIntegrationConfiguration>();
    services.AddElittaService(opts.Endpoint, opts.Username, opts.Password);
}
```


#### **HomeController.cs**

> Bielitta servisini kullanmak istedi�iniz yerde a�a��daki �ekilde Dependency Injection yaparak kullan�m�n� sa�layabilirsiniz.

�rnek olarak; belli bir e-mail adrese ait elitta sisteminde kay�tl� m��teri verisi �ekilmesi(GetByEmail) e�er kay�t bulunamazsa, m��teri kayd�n�n olu�turulmas�n�n(CreateIndividualAccount) sa�lanmas�. Ard�ndan elitta servisi i�in kulland���n�z hesap ile ili�kili firma verisinin �ekilmesi(GetCompanies) ve elde edilen firmalardan ilk firmadan kay�t edilen/elde edilen m��teriye puan aktar�m�(WinElitta) sa�lanmas� �rneklenmi�tir.

```csharp
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
```


#### **Home/Index.cshtml**
View kodunda bir detay bulunmamaktad�r.

```html
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    @if (ViewBag.Result != null)
    {
        @Html.Raw(ViewBag.Result)
    }
</div>

```