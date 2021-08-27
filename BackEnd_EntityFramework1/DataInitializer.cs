using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_EntityFramework1
{
    //DropCreateDatabaseIfModelChanges
    //DropCreateDatabaseAlways
    public class DataInitializer: DropCreateDatabaseAlways<VeriTabaniContext>
    {



        protected override void Seed(VeriTabaniContext context)
        {
            List<Urun> urunler = new List<Urun>()
            {
                new Urun()
                {
                    UrunAdi = "Samsung S4",
                    Fiyat = 1294,
                    StockAdedi = 100,
                    SatistaMi = true,
                },
                new Urun()
                {
                    UrunAdi = "IPhone X",
                    Fiyat = 3000,
                    StockAdedi = 200,
                    SatistaMi = false,
                },
                new Urun()
                {
                    UrunAdi = "IPhone 11",
                    Fiyat = 5000,
                    StockAdedi =300,
                    SatistaMi = true,
                },
                new Urun()
                {
                    UrunAdi = "Siomai 4",
                    Fiyat = 915,
                    StockAdedi =50,
                    SatistaMi = true,
                },
            };

            foreach (var item in urunler)
            {
                context.Urunler.Add(item);
            }
            context.SaveChanges();


            List<Kategori> kategoriler = new List<Kategori>() {
                new Kategori(){KategoriAdi="Telefon"},
                new Kategori(){KategoriAdi="Tablet"},
                new Kategori(){KategoriAdi="Televizyon"},
                new Kategori(){KategoriAdi="Laptop"},
                new Kategori(){KategoriAdi="Bilgisayar"},

            };

            foreach (var item in kategoriler)
            {
                context.Kategoriler.Add(item);
            }
            context.SaveChanges();

            base.Seed(context);
        }

    }
}
