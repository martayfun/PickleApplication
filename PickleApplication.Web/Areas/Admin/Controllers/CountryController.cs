using PickleApplication.BusinessLayer.Models;
using PickleApplication.BusinessLayer.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Areas.Admin.Controllers
{
    public class CountryController : Controller
    {
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public async Task<ActionResult> All()
        {
            return View(await _countryService.GetCountries());
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int? id)
        {
            if (id.HasValue)
                return View(await _countryService.GetCountryById((int)id));
            return View();
        }

        [HttpPost]
        public ActionResult Modify(Country country)
        {
            return View();
        }

        private readonly ICountryService _countryService;
    }
}