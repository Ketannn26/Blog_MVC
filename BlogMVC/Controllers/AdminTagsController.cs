using BlogMVC.Data;
using BlogMVC.Models.Domain;
using BlogMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Controllers
{
    public class AdminTagsController : Controller
    {
        private BlogDbContext blogdbContext;
        public AdminTagsController(BlogDbContext blogDbContext) 
        {
          this.blogdbContext = blogDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest atr)
        {
            var tag = new Tag
            {
                Name = atr.Name,
                DisplayName = atr.DisplayName,
            };

            blogdbContext.Add(tag);
            blogdbContext.SaveChanges();
            // return View("Add");
            // return View("List");
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult List()
        {
            var tags = blogdbContext.Tags.ToList();
            return View(tags);
        }
    }
}
