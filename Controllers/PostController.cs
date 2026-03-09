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
            var posts = _context.Posts.OrderByDescending(p => p.CreatedAt).ToList();
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
        public async Task<IActionResult> Create(Post post, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/images/posts", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    post.ImagePath = "/images/posts/" + fileName;
                }

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(post);
        }
    }
}