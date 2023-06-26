using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index(int sayfa = 1, string aranan = "", string tittleSiralama = "", string kategoriFiltreleme = "", string etiketFiltreleme = "", string yayinFiltreleme = "")
        {
            var db = new SqlContext();
            var blogs = db.Blogs.Include(x => x.BlogCategory)
                .Include(x => x.BlogTags)
                .Where(x => EF.Functions.Like(x.Tittle, "%" + aranan + "%"))
                .Where(x => EF.Functions.Like(x.BlogCategory.CategoryName, "%" + kategoriFiltreleme + "%"))
                .OrderBy(x => x.Tittle).ToList();



            if (tittleSiralama == "asc")
            {
                blogs = blogs.OrderBy(x => x.Tittle).ToList();
            }
            else if (tittleSiralama == "desc")
            {
                blogs = blogs.OrderByDescending(x => x.Tittle).ToList();
            }



            if (yayinFiltreleme == "true")
            {
                blogs = blogs.Where(x => x.Publish == true).ToList();
            }
            else if (yayinFiltreleme == "false")
            {
                blogs = blogs.Where(x => x.Publish == false).ToList();
            }



            double kayitSayisi = Convert.ToDouble(blogs.Count());
            int sayfaSayisi = Convert.ToInt32(Math.Ceiling(kayitSayisi / 5));

            ViewBag.SayfaSayisi = sayfaSayisi;
            ViewBag.OncekiSayfa = sayfa == 1 ? 1 : sayfa - 1;
            ViewBag.SonrakiSayfa = sayfa == sayfaSayisi ? sayfaSayisi : sayfa + 1;
            ViewBag.Aranan = aranan;
            ViewBag.Sayfa = sayfa;
            ViewBag.TittleSiralama = tittleSiralama;
            ViewBag.Kategoriler = db.Categories.ToList();
            ViewBag.Etiketler = db.Tags.ToList();
            ViewBag.KategoriFiltreleme = kategoriFiltreleme;
            ViewBag.YayinFiltreleme = yayinFiltreleme;


            blogs = blogs.Skip((sayfa - 1) * 5)
                .Take(5).ToList();

            return View(blogs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var db = new SqlContext();

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Tags = db.Tags.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogCreate blogCreate)
        {
            var db = new SqlContext();

            if (ModelState.IsValid)
            {
                var blog = new Blog();

                if (blogCreate.Publish == "true")
                {
                    blog.Publish = true;
                }
                else if (blogCreate.Publish == "false")
                {
                    blog.Publish = false;
                }

                blog.PublishDate = DateTime.Now;
                blog.Tittle = blogCreate.Tittle;
                blog.Content = blogCreate.Content;
                blog.BlogCategoryId = blogCreate.BlogCategoryId;

                var tags = db.Tags.Where(x => blogCreate.BlogTagsId.Contains(x.Id)).ToList();
                blog.BlogTags.AddRange(tags);


                db.Blogs.Add(blog);
                db.SaveChanges();


                ModelState.Clear();
                ViewBag.message = "Kayıt başarılı";

            }

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Tags = db.Tags.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult BlogAssignTag(int id)
        {
            var db = new SqlContext();

            var blog = db.Blogs.Include(x => x.BlogCategory)
                .Include(x => x.BlogTags).FirstOrDefault(x => x.Id == id);

            ViewBag.Tags = db.Tags.ToList();

            return View(blog);
        }

        [HttpPost]
        public IActionResult BlogAssignTag(int[] tagsId, int blogId)
        {
            var db = new SqlContext();

            var blog = db.Blogs.Find(blogId);
            var tags = db.Tags.Where(x => tagsId.Contains(x.Id)).ToList();
            blog.BlogTags.AddRange(tags);
            db.SaveChanges();


            ViewBag.Tags = db.Tags.ToList();
            ViewBag.message = "Etiket başarı ile kaydedildi.";


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var db = new SqlContext();
            var blog = db.Blogs.Find(id);


            return View(blog);
        }

        [HttpPost]
        public IActionResult Delete(Blog blog)
        {
            var db = new SqlContext();
            db.Blogs.Remove(blog);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var db = new SqlContext();
            var blog = db.Blogs.Include(x => x.BlogCategory)
                .Include(x => x.BlogTags).FirstOrDefault(x => x.Id == id);

            ViewBag.BlogId = id;
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Tags = db.Tags.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Update(BlogCreate blogCreate, int blogId)
        {
            var db = new SqlContext();

            if (ModelState.IsValid)
            {
                //var eskiBlog = db.Blogs.Find(blogId);
                //eskiBlog.BlogTags.Clear();
                //db.SaveChanges();

                var blog = new Blog();
                blog.Id = blogId;

                if (blogCreate.Publish == "true")
                {
                    blog.Publish = true;
                }
                else if (blogCreate.Publish == "false")
                {
                    blog.Publish = false;
                }

                blog.PublishDate = DateTime.Now;
                blog.Tittle = blogCreate.Tittle;
                blog.Content = blogCreate.Content;
                blog.BlogCategoryId = blogCreate.BlogCategoryId;

                var tags = db.Tags.Where(x => blogCreate.BlogTagsId.Contains(x.Id)).ToList();
                blog.BlogTags.AddRange(tags);

                db.Blogs.Update(blog);
                db.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}
