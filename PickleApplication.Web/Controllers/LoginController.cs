using Newtonsoft.Json;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.BusinessLayer.Services;
using PickleApplication.BusinessLayer.Utils;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PickleApplication.Web.Controllers
{
    public class LoginController : Controller
    {
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if(Request.Cookies["User"] != null)
            {
                User user = JsonConvert.DeserializeObject<User>(Request.Cookies["User"].Value);
                if (user.UserId > 0)
                    Response.Redirect("~/admin/Order/All");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(User user)
        {
            if(ModelState.IsValid)
            {
                var usr = await _userService.GetByUsernameAndPassword(user);
                if(usr != null && usr.UserId > 0)
                {
                    var cookie = new HttpCookie("User");
                    cookie.Value = Common.FromObjectWithCamelCasePropertyNames(usr);
                    cookie.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(cookie);
                    ViewData["Message"] = "";
                    return Redirect("~/admin/AdmProduct/All");
                }
                else
                {
                    ViewData["Message"] = "Lütfen kullanıcı adınızı ve şifrenizi kontrol ederek tekrar giriş yapmayı deneyiniz!";
                    return View(usr);
                }
            }
            else
            {
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult Singout()
        {
            var cookie = new HttpCookie("User");
            cookie.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Add(cookie);
            return Redirect("~/Login/Index");
        }
        private readonly IUserService _userService;
    }
}