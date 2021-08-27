using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_EntityFramework1
{
    class Program
    {
        static void Main(string[] args)
        {

            // LINQ (Language integrated Query)
            VeriTabaniContext context = new VeriTabaniContext();

            /*
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

            foreach (var urun in urunler)
            {
                context.Urunler.Add(urun);
            }
            context.SaveChanges();
            */

            /*

            // List<Kategori> kategoriler = context.Kategoriler.ToList();
            var kategoriler = context.Kategoriler.ToList();

            Console.WriteLine("Kategoriler: ");
            foreach (var kategori in kategoriler)
            {
                Console.WriteLine("Kategori id : {0}, Kategori Adi : {1}", kategori.Id, kategori.KategoriAdi);
            }

            Console.WriteLine("Urunler: ");
            var urunler = context.Urunler.ToList();

            foreach (var urun in urunler)
            {
                Console.WriteLine("Urun adi : {0}, Urun Fiyatı : {1}, Urun Stock Adedi : {2}", urun.UrunAdi, urun.Fiyat,urun.StockAdedi);
            }


            Console.WriteLine("\nBir tane urun bulup yazma Seçme \n");

            // Urun Id sine göre bulma
            var urun1 = context.Urunler.Find(5);

            Console.WriteLine("Urun adi : {0}, Urun Fiyatı : {1}, Urun Stock Adedi : {2}", urun1.UrunAdi, urun1.Fiyat, urun1.StockAdedi);
            Console.WriteLine("Database'e yazıldı");


            // Güncelleme yapmak
            Console.WriteLine("\n Id si 1 olan urunu bulup değiştirip güncelleme \n");
            var urun2 = context.Urunler.Find(1);
            urun2.Fiyat += 99;
            urun2.StockAdedi -= 45;
            context.SaveChanges();

            urunler = context.Urunler.ToList();
            Console.WriteLine("\nKategoriler: ");
            foreach (var urun in urunler)
            {
                Console.WriteLine("Urun adi : {0}, Urun Fiyatı : {1}, Urun Stock Adedi : {2}", urun.UrunAdi, urun.Fiyat, urun.StockAdedi);
            }
            Console.ReadLine();


            var urun3 = context.Urunler.Find(4);
            if(urun3 != null)
            {
                context.Urunler.Remove(urun3);
            }
            context.SaveChanges();

            */
            var urunler = context.Urunler.ToList();
            Console.WriteLine("Veritabani Oluştu");

            Console.ReadLine();

        }
    }
}
