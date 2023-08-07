using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokUygulamasıMVC.Models.Entity;
using PagedList;
using PagedList.Mvc;
using System.Security.Cryptography;
using StokUygulamasıMVC.Models.Entity;

namespace StokUygulaması.Controllers
{
    public class UrunlerController : Controller
    {
        ÜrünStokDatabaseEntities ürünStokEntities = new ÜrünStokDatabaseEntities();
        public ActionResult Index(string p,int sayfa = 1)
        {
           var değerler = from ürün in ürünStokEntities.URUNLER select ürün;
           if(!string.IsNullOrEmpty(p))
            {
                değerler = değerler.Where(m => m.URUNAD.Contains(p));
            }
           var urunList = değerler.ToList().ToPagedList(sayfa, 6);

            return View(urunList);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> kategorilerim = (from i in ürünStokEntities.KATEGORILER.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.KATEGORIAD,
                                                      Value = i.KATEGORIID.ToString()
                                                  }).ToList();

            ViewBag.kategori = kategorilerim;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(URUNLER urun)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniUrun");
            }

            using (ÜrünStokDatabaseEntities dbContext = new ÜrünStokDatabaseEntities())
            {
                // Ürünün ait olduğu kategori nesnesini alın
                var ktg = dbContext.KATEGORILER.Where(m => m.KATEGORIID == urun.KATEGORILER.KATEGORIID).FirstOrDefault();
                int i = ürünStokEntities.URUNLER.Count();


                // Eğer ktg null değilse (kategori bulunduysa), urun nesnesinin KATEGORILER özelliğine atanır
                if (ktg != null)
                {
                    urun.KATEGORILER = ktg;
                    urun.URUNID = i+1;

                    // URUNLER tablosuna yeni ürünü ekleyin
                    dbContext.URUNLER.Add(urun);
                    dbContext.SaveChanges();
                }

                // Index sayfasına yönlendirin
                return RedirectToAction("Index");
            }
        }
        public ActionResult Sil(int id)
        {
            var bulunanUrun = ürünStokEntities.URUNLER.Find(id);
            ürünStokEntities.URUNLER.Remove(bulunanUrun);
            ürünStokEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Urungetir(int id)
        {
            var bulunanUrun = ürünStokEntities.URUNLER.Find(id);
            List<SelectListItem> kategorilerim = (from i in ürünStokEntities.KATEGORILER.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.KATEGORIAD,
                                                      Value = i.KATEGORIID.ToString()
                                                  }).ToList();
            ViewBag.kategori = kategorilerim;
            return View("UrunGuncelle", bulunanUrun);
        }
        [HttpPost]
        public ActionResult UrunGuncelle(URUNLER urun)
        {
            var bulunanUrun = ürünStokEntities.URUNLER.Find(urun.URUNID);

            bulunanUrun.URUNID = urun.URUNID;
            bulunanUrun.URUNAD = urun.URUNAD;
            bulunanUrun.MARKA = urun.MARKA;
            var ktg = ürünStokEntities.KATEGORILER.Where(m => m.KATEGORIID == urun.KATEGORILER.KATEGORIID).FirstOrDefault();
            bulunanUrun.URUNKATEGORI = (short?)ktg.KATEGORIID;
            bulunanUrun.FIYAT = urun.FIYAT;
            bulunanUrun.STOK = urun.STOK;
            ürünStokEntities.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}