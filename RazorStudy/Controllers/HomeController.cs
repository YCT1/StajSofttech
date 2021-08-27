using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorStudy.Models;
namespace RazorStudy.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // Control Veri tabanına bağlıdır


           
            UrunKategoriModel model = new UrunKategoriModel();
            model.Urunler = Veritabani.Liste;
            model.UrunSayisi = Veritabani.Liste.Where(i=> i.SatistaMi == true).Count();

            //ViewBag.kategori = kategori;
            //ViewBag.UrunSayisi = urunler.Count();
            return View(model);
        }

        public ActionResult Details(int id)
        {

            var urun = Veritabani.Liste.Where(i => i.UrunID == id).FirstOrDefault();
            return View(urun);
        }
        public ActionResult UrunEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(string UrunAdi, string Aciklama, double Fiyat,  string Resim, bool SatistaMi)
        {
            Urun entity = new Urun();
            entity.UrunAdi = UrunAdi;
            entity.Aciklama = Aciklama;
            entity.Fiyat = Fiyat;
            entity.Resim = Resim;
            entity.SatistaMi = SatistaMi;
            entity.UrunID = Veritabani.Liste.Count()+1;
            Veritabani.ElemanEkle(entity);
            UrunKategoriModel model = new UrunKategoriModel();
            model.Urunler = Veritabani.Liste;
            model.UrunSayisi = Veritabani.Liste.Where(i => i.SatistaMi == true).Count();
            // Postan veri gelecek
            return View("Index", model);
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}