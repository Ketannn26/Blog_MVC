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
         [HttpGet]
           public IActionResult Edit(Guid id)
           {
               var tag = blogdbContext.Tags.Find(id);
               if (tag == null)
               {
                   var etr = new EditTagRequest
                   {
                       id = tag.Id,
                       Name = tag.Name,
                       DisplayName = tag.DisplayName,
                   };
                   return View(etr);
               }
               return View(null);
           }
        [HttpPost]
        public IActionResult Edit(EditTagRequest etr)
        {
            var tag = new Tag
            {
                Id = etr.id,
                Name = etr.Name,
                DisplayName = etr.DisplayName
            };
            var oldTag = blogdbContext.Tags.Find(tag.Id);
            if (oldTag != null)
            {
                oldTag.Name = etr.Name;
                oldTag.DisplayName = etr.DisplayName;
                blogdbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction(null);
        }
    }
}
