using PickleApplication.BusinessLayer.Models;
using PickleApplication.BusinessLayer.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Areas.Admin.Controllers
{
    public class RegionController : Controller
    {
        public RegionController(IRegionService regionService, ICityService cityService)
        {
            _regionService = regionService;
            _cityService = cityService;
        }
        public async Task<ActionResult> All()
        {
            if (TempData["DeleteStatus"] != null) ViewBag.DeleteStatus = (bool)TempData["DeleteStatus"];
            return View(await _regionService.GetRegions());
        }

        public async Task<ActionResult> Modify(int? id)
        {
            ViewBag.Cities = (await _cityService.GetCities()).ToDictionary(c => c.CityId, c => c.CityName);
            if (id.HasValue)
                return View(await _regionService.GetRegionById((int)id));
            else
                return View();
        }
        [HttpPost]
        public async Task<ActionResult> Modify(Region region)
        {
            ViewBag.Cities = (await _cityService.GetCities()).ToDictionary(c => c.CityId, c => c.CityName);
            if (ModelState.IsValid)
            {
                if (region.RegionId > 0)
                {
                    ViewBag.UpdateStatus = await _regionService.UpdateRegion(region);
                }
                else
                {
                    ViewBag.CreateStatus = await _regionService.CreateRegion(region);
                }
                ModelState.Clear();
                return View(new Region());
            }
            else
                return View(region);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["DeleteStatus"] = await _regionService.DeleteRegionById(id);
            return RedirectToAction("all");
        }

        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
    }
}