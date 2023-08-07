using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokUygulamasıMVC.Models.Entity;
using PagedList;
using PagedList.Mvc;
using StokUygulamasıMVC.Models.Entity;

namespace StokUygulaması.Controllers
{
    public class KategoriController : Controller
    {
        private ÜrünStokDatabaseEntities ürünStokEntities = new ÜrünStokDatabaseEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var kategoriList = ürünStokEntities.KATEGORILER.ToList().ToPagedList(sayfa, 6);
            return View(kategoriList);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(KATEGORILER yenikategori)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }

            ürünStokEntities.KATEGORILER.Add(yenikategori);
            ürünStokEntities.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var bulunanId = ürünStokEntities.KATEGORILER.Find(id);
            ürünStokEntities.KATEGORILER.Remove(bulunanId);
            ürünStokEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var bulunanKategori = ürünStokEntities.KATEGORILER.Find(id);
            return View("KategoriGetir", bulunanKategori);
        }
        public ActionResult Guncelle(KATEGORILER kategori)
        {
            var ktg = ürünStokEntities.KATEGORILER.Find(kategori.KATEGORIID);
            ktg.KATEGORIAD = kategori.KATEGORIAD;
            ürünStokEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}