using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_EntityFramework1
{
    public class Urun
    {
        // Varsayılan olarak otomatik sayı olarak ayarlarlanır
        // Id manual set edilmesi gerekmez
        // Birinci uninuq key olarak eşitlenir
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public double Fiyat { get; set; }
        public int StockAdedi { get; set; }

        public bool SatistaMi { get; set; }


        // Foreing Keys

        public int KategoriId { get; set; }

        // Burası program anında kullanılacak
        public Kategori Kategori { get; set; }
        public List<Tedarikci> Tedarikciler { get; set; }

    }
}
