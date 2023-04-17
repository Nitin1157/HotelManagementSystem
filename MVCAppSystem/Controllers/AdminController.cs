using Microsoft.AspNetCore.Mvc;

namespace MVCAppSystem.Controllers
{
	public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AdminController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
		{
            var user = _contextAccessor.HttpContext.Session.GetString("email");
            if (user == null)
            {
                return RedirectToAction("SignIn", "Logins");
            }
            return View();
		}
	}
}
