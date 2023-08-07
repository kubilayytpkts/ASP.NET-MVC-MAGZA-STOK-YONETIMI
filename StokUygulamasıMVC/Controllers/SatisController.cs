using StokUygulamasıMVC.Models.Entity;
using StokUygulamasıMVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StokUygulaması.Controllers
{
    public class SatisController : Controller
    {
        ÜrünStokDatabaseEntities ürünStokEntities = new ÜrünStokDatabaseEntities();
        public int satısId;
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]//INDEX View içerisinde kullanıyoruz 
        public ActionResult YeniSatis()
        {
            List<SelectListItem> kategoriler = (from i in ürünStokEntities.MUSTERILER.ToList()
                                                select new SelectListItem
                        
                                                {
                                                    Text = i.MUSTERIAD + i.MUSTERISOYAD,
                                                    Value = i.MUSTERIID.ToString()

                                                }).ToList();
            ViewBag.kategori = kategoriler;
            return View();
        }

        [HttpPost]//INDEX View içerisinde kullanıyoruz 
        public ActionResult YeniSatis(SATISLAR satis)
        {
            int i = ürünStokEntities.SATISLAR.Count();
            satis.SATISID = i + 1;
            ürünStokEntities.SATISLAR.Add(satis);
            ürünStokEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}