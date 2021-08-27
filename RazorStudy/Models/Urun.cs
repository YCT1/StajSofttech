using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RazorStudy.Models
{
    public class Urun
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }

        public string Aciklama { get; set; }

        public double Fiyat { get; set; }

        public bool SatistaMi { get; set; }

        public string Resim { get; set; }

    }
}