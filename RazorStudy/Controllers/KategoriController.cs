using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorStudy.Models;
namespace RazorStudy.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        public ActionResult Index()
        {

           
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult KategoriMenu()
        {
            List<Kategori> kategori = new List<Kategori>()
            {
                new Kategori(){Id=1,KategoriAdi="Smart Phones"},
                new Kategori(){Id=2,KategoriAdi="Computers"},
                new Kategori(){Id=2,KategoriAdi="TVs"},
            };
            return PartialView("KategoriMenu", kategori);
        }
    }
}