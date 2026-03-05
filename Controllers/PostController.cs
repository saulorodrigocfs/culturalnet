using Microsoft.AspNetCore.Mvc;
using ModelPost;

namespace CulturalNet.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}