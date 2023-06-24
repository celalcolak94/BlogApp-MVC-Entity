using BlogApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            var db = new SqlContext();

            ViewBag.Categories = db.Categories.ToList();

            return View();
        }


        [HttpPost]
        public IActionResult Index(Category category)
        {
            var db = new SqlContext();

            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.message = "Kategori başarı ile kayıt edildi.";

            }

            ViewBag.Categories = db.Categories.ToList();

            return View();
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var db = new SqlContext();
            var c = db.Categories.Find(id);

            return View(c);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                var db = new SqlContext();
                db.Categories.Update(category);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.message = "Güncelleme işlemi başarılı.";

            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var db = new SqlContext();
            var c = db.Categories.Find(id);

            return View(c);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {

            var db = new SqlContext();
            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
