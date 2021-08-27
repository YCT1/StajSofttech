using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_EntityFramework1
{
    public class VeriTabaniContext:DbContext
    {

        public VeriTabaniContext():base("VeritabaniConnection")
        {
            Database.SetInitializer(new DataInitializer());
        }
        public DbSet<Kategori> Kategoriler { get; set; }

        public DbSet<Urun> Urunler { get; set; }


    }
}
