using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokUygulamasıMVC.Models.Entity;
using StokUygulamasıMVC.Models.Entity;

namespace StokUygulaması.Controllers
{
    public class MusteriController : Controller
    {
        ÜrünStokDatabaseEntities ürünStokEntities = new ÜrünStokDatabaseEntities();
        public ActionResult Index(string p)
        
        {
            var degerler = from musteri in ürünStokEntities.MUSTERILER select musteri;
            if(!string.IsNullOrEmpty(p))
            {
                degerler=degerler.Where(m=>m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());

        }
        [HttpGet]
        public ActionResult YeniMüsteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMüsteri(MUSTERILER müsteri)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMüsteri");
            }
            ürünStokEntities.MUSTERILER.Add(müsteri);
            ürünStokEntities.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var bulunanMusteri = ürünStokEntities.MUSTERILER.Find(id);
            ürünStokEntities.MUSTERILER.Remove(bulunanMusteri);
            ürünStokEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var bulunanMusteri = ürünStokEntities.MUSTERILER.Find(id);
            return View("MusteriGuncelle", bulunanMusteri);
        }
        public ActionResult MusteriGuncelle(MUSTERILER musteri)
        {
            var BulunanId = ürünStokEntities.MUSTERILER.Find(musteri.MUSTERIID);
            BulunanId.MUSTERIAD = musteri.MUSTERIAD;
            BulunanId.MUSTERISOYAD = musteri.MUSTERISOYAD;
            ürünStokEntities.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}