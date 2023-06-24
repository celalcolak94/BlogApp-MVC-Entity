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
                .ThenInclude(x => x.Tag)
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

            ViewBag.categories = db.Categories.ToList();
            ViewBag.tags = db.Tags.ToList();

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


                db.Blogs.Add(blog);
                db.SaveChanges();


                foreach (var item in blogCreate.BlogTagsId)
                {
                    var blogtag = new BlogTag();
                    blogtag.BlogId = blog.Id;
                    blogtag.TagId = item;

                    db.BlogTags.Add(blogtag);
                    db.SaveChanges();
                }



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

            var blog = db.Blogs.Find(id);

            ViewBag.Tags = db.Tags.ToList();
            ViewBag.Blogs = db.Blogs.ToList();
            ViewBag.BlogId = blog.Id;


            return View();
        }

        [HttpPost]
        public IActionResult BlogAssignTag(BlogTag blogtag)
        {
            var db = new SqlContext();

            ViewBag.Tags = db.Tags.ToList();
            ViewBag.Blogs = db.Blogs.ToList();
            ViewBag.message = "Etiket başarı ile kaydedildi.";

            db.BlogTags.Add(blogtag);
            db.SaveChanges();

            return View();
        }
    }
}
