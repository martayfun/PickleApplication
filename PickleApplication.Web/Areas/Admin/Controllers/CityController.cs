using PickleApplication.BusinessLayer.Models;
using PickleApplication.BusinessLayer.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Areas.Admin.Controllers
{
    public class CityController : Controller
    {
        public CityController(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }
        public async Task<ActionResult> All()
        {
            if (TempData["DeleteStatus"] != null) ViewBag.DeleteStatus = (bool)TempData["DeleteStatus"];
            return View(await _cityService.GetCities());
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int? id)
        {
            ViewBag.Countries = (await _countryService.GetCountries()).ToDictionary(c => c.CountryId, c => c.CountryName);
            if (id.HasValue)
                return View(await _cityService.GetCityById((int)id));
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Modify(City city)
        {
            ViewBag.Countries = (await _countryService.GetCountries()).ToDictionary(c => c.CountryId, c => c.CountryName);
            if (ModelState.IsValid)
            {
                if (city.CityId > 0)
                    ViewBag.UpdateStatus = await _cityService.UpdateCity(city);
                else
                    ViewBag.CreateStatus = await _cityService.CreateCity(city);
                ModelState.Clear();
                return View(new City());
            }
            return View(city);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["DeleteStatus"] = await _cityService.DeleteCityById(id);
            return RedirectToAction("all");
        }

        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
    }
}