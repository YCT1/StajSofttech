using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RazorStudy.Models
{
    public static class Veritabani
    {
        private static List<Urun> _liste;

        static Veritabani()
        {
            _liste = new List<Urun>() {
                new Urun(){UrunID=1, UrunAdi="Iphone 8", Aciklama="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", Fiyat=9999, SatistaMi=true,Resim="resim1.jpg"},
                new Urun(){UrunID=2, UrunAdi="PocoPhone", Aciklama="Pro China Movement, Join", Fiyat=1450, SatistaMi=true,Resim="resim1.jpg"},
                new Urun(){UrunID=3, UrunAdi="Iphone X", Aciklama="Infinite Display, Here we come", Fiyat=11200, SatistaMi=true,Resim="resim2.jpg"},
                new Urun(){UrunID=4, UrunAdi="Samsung S20", Aciklama="The king of the Android", Fiyat=4500, SatistaMi=true,Resim="resim1.jpg"},
                new Urun(){UrunID=5, UrunAdi="LG G4", Aciklama="Love for the old school", Fiyat=484, SatistaMi=false, Resim="resim1.jpg"},

            };
        }



        public static List<Urun> Liste
        {
            get { return _liste; }
        }


        public static void ElemanEkle(Urun entity)
        {
            _liste.Add(entity);
        }

        public static Urun UrunDetay(int urunid)
        {
            Urun etity = null;
            foreach (var urun in _liste)
            { 
                 if(urun.UrunID == urunid)
                {
                    etity = urun;
                    break;
                }
            }
            return etity;
        }
    }
}