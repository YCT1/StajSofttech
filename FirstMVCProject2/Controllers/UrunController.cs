using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstMVCProject2.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        public string Index()
        {
            return "Urun Index";
        }

        // Urun/Liste
        public ViewResult Liste()
        {
            return View();
        }

        public string UrunDetay()
        {
            return "Urun UrunDetay";
        }
    }
}