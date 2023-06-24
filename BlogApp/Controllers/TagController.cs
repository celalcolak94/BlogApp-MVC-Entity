using BlogApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BlogApp.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            var db = new SqlContext();
            ViewBag.Tags = db.Tags.ToList();

            return View();
        }


        [HttpPost]
        public IActionResult Index(Tag tag)
        {
            var db = new SqlContext();

            if (ModelState.IsValid)
            {
                db.Tags.Add(tag);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.message = "Etiket başarı ile kayıt edildi.";

            }

            ViewBag.Tags = db.Tags.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var db = new SqlContext();
            var t = db.Tags.Find(id);

            return View(t);
        }

        [HttpPost]
        public IActionResult Update(Tag tag)
        {
            if (ModelState.IsValid)
            {
                var db = new SqlContext();
                db.Tags.Update(tag);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.message = "Etiket başarı ile güncellendi";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var db = new SqlContext();
            var t = db.Tags.Find(id);

            return View(t);
        }

        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
            var db = new SqlContext();
            db.Tags.Remove(tag);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
