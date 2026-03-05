using CulturalNet.Data;
using Microsoft.AspNetCore.Mvc;
using ModelPost;

namespace CulturalNet.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;

        public PostController(AppDbContext context)
        {
            _context = context;
        }

        // FEED PRINCIPAL
        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            return View(posts);
        }

        //ABRIR FORMULÁRIO
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // RECEBER FORMULÁRIO
        [HttpPost]
        public IActionResult Create(Post post)
        {
            //Console.WriteLine("POST RECEBIDO");

            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(post);
        }
    }
}