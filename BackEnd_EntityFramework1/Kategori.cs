using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_EntityFramework1
{
    public class Kategori
    {
        // Varsayılan olarak otomatik sayı olarak ayarlarlanır
        // Id manual set edilmesi gerekmez
        // Birinci uninuq key olarak eşitlenir

        public int Id { get; set; }
        public string KategoriAdi { get; set; }


        // Burası program anında kullanılacak
        public List<Urun> Urunler { get; set; }
    }
}
