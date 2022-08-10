using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OTMF_NETCORE_MVC.Controllers
{
    public class ModulosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Administracion()
        {
            return Content("<h1>Admin module</h1>","text/html");
        }
        [Authorize(Roles = "Gerencia")]
        public IActionResult Gerencia()
        {
            return Content("<h1>Gerencia module</h1>","text/html");
        }
    }
}
