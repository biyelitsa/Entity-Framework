using PersonelDbFirst.Connection;
using PersonelDbFirst.Models.Data;
using PersonelDbFirst.Models.DTO;
using PersonelDbFirst.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelDbFirst.Controllers
{
    public class PersonelController : Controller
    {
        static perdb3Entities db = DbSingleTone.GetConnection();

        // GET: Personel

        public ActionResult List()
        {
            var pList = db.Set<Personel>().Select(x => new PersonelDTO
                {
                Ad = x.Ad,
                Ikamet = x.Ulke.UlkeAd,
                Id = x.PersonelId,
                Soyad = x.Soyad,
                UnvanAd = x.Unvan.UnvanAd,
                Yonetici = x.Personel2.Ad + " " + x.Personel2.Soyad,
                UnvanId = x.UnvanId?? 0
                }).Distinct().ToList();
            return View("List", pList);
        }
        public ActionResult ListByUnvanId(int id)
        {
            var pList = db.Set<Personel>().Select(x => new PersonelDTO
            {
                Ad = x.Ad,
                Ikamet = x.Ulke.UlkeAd,
                Id = x.PersonelId,
                Soyad = x.Soyad,
                UnvanAd = x.Unvan.UnvanAd,
                Yonetici = x.Personel2.Ad + " " + x.Personel2.Soyad,
                UnvanId = x.UnvanId?? 0
            }).Where(x=> x.UnvanId == id).Distinct().ToList();
            return View("List",pList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Session["Error"] = $" ";
            PersonelModel model = new PersonelModel();
            model.Baslik = "Yeni Giriş";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Kaydet";
            model.Type = "hidden";
            model.Personel = new Personel();
            model.UlkeList = GetUlkeList();
            model.UnvanList = GetUnvanList();
            model.YoneticiList = GetYoneticiList();
            model.Type = "hidden";
            return View("crud",model);
        }
        [HttpPost]
        public ActionResult Create(PersonelModel model)
        {
            db.Entry(model.Personel).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Session["Error"] = $" ";
            PersonelModel model = new PersonelModel();
            model.Baslik = "Güncelleme";
            model.BtnClass = "btn btn-success";
            model.BtnVal = "Güncelle";
            model.Type = "hidden";
            model.Personel = db.Set<Personel>().Find(id);
            model.UlkeList = GetUlkeList();
            model.UnvanList = GetUnvanList();
            model.YoneticiList = GetYoneticiList();
            model.Type = "hidden";
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Update(PersonelModel model)
        {
            //Personel p = db.Set<Personel>().Find(model.Personel.PersonelId);

            //db.Entry(model.Personel).State = EntityState.Modified;
            //db.SaveChanges();
            //return RedirectToAction("List");

            //db.Entry(model.Personel).State = EntityState.Unchanged;
            Personel p = db.Set<Personel>().Find(model.Personel.PersonelId);
            p.Ad = model.Personel.Ad;
            p.Maas = model.Personel.Maas;
            p.UnvanId = model.Personel.UnvanId;
            p.YöneticiId = model.Personel.YöneticiId;
            p.UyrukId = model.Personel.UyrukId;
            p.UlkeId = model.Personel.UlkeId;
            db.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Session["Error"] = $" ";
            PersonelModel model = new PersonelModel();
            model.Baslik = "Silme";
            model.BtnClass = "btn btn-danger";
            model.BtnVal = "Sil";
            model.Type = "hidden";
            model.Personel = db.Set<Personel>().Find(id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Delete(Personel model)
        {
            try
            {
                Session["Error"] = null;
                Personel p = db.Set<Personel>().Find(model.PersonelId);
                db.Personels.Remove(p);
                //db.Unvans.Remove(model);
                //db.Set<Unvan>().Remove(model);
                //db.Entry(model).State = EntityState.Deleted;
                //db.SaveChanges();
            }
            catch (Exception)
            {
                //ViewBag.Error = ex.Message;

                Session["Error"] = $"{model.PersonelId} kaydının bağlantısı bulunmaktadır. Silinemez.";
                Session["cls"] = "danger";
            }
            db.SaveChanges();
            return RedirectToAction("List");

        }
        private List<PersonelSelect> GetYoneticiList()
        {
            return db.Set<Personel>().Select(x => new PersonelSelect
            {
                YoneticiId = x.YöneticiId?? 0,
                AdSoy = x.Personel2.Ad + " " + x.Personel2.Soyad
            }).Where(x=> x.YoneticiId!=0).Distinct().ToList();
        }

        private List<UnvanDTO> GetUnvanList()
        {
            return db.Set<Unvan>().Select(x => new UnvanDTO
            {
                UnvanId = x.UnvanId,
                UnvanAd = x.UnvanAd
            }).ToList();
        }

        private List<UlkeDTO> GetUlkeList()
        {
            return db.Set<Ulke>().Select(x=> new UlkeDTO
            { 
                UlkeId=x.UlkeId,
                UlkeAd=x.UlkeAd
            }).ToList();
        }
    }
}