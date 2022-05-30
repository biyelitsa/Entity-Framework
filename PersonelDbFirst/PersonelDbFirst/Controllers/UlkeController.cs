using PersonelDbFirst.Connection;
using PersonelDbFirst.Models.Data;
using PersonelDbFirst.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelDbFirst.Controllers
{
    public class UlkeController : Controller
    {
        // GET: Ulke
        perdb3Entities db = DbSingleTone.GetConnection();
        public ActionResult List()
        {
            //var uList = db.Unvans.ToList();
            var uList = db.Set<Ulke>().ToList();
            var ulkeler = db.Set<Ulke>().FirstOrDefault().Personels;
            return View(uList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Session["Error"] = $" ";
            UlkeModel model = new UlkeModel();
            model.Baslik = "Yeni Giriş";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Kaydet";
            model.Type = "hidden";
            model.Ulke = new Ulke();
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Create(Ulke model)
        {
            //db.Unvans.Add(model);
            //db.Set<Unvan>().Add(model);
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Session["Error"] = $" ";
            UlkeModel model = new UlkeModel();
            model.Baslik = "Silme";
            model.BtnClass = "btn btn-danger";
            model.BtnVal = "Sil";
            model.Type = "hidden";
            model.Ulke = db.Set<Ulke>().Find(id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Delete(Ulke model)
        {
            try
            {
                Session["Error"] = null;
                Ulke p = db.Set<Ulke>().Find(model.UlkeId);
                db.Ulkes.Remove(p);
                //db.Unvans.Remove(model);
                //db.Set<Unvan>().Remove(model);
                //db.Entry(model).State = EntityState.Deleted;
                //db.SaveChanges();
            }
            catch (Exception)
            {
                //ViewBag.Error = ex.Message;

                Session["Error"] = $"{model.UlkeId} kaydının bağlantısı bulunmaktadır. Silinemez.";
                Session["cls"] = "danger";
            }
            db.SaveChanges();
            return RedirectToAction("List");

        }
        [HttpGet]
        public ActionResult Update(string id)
        {
            Session["Error"] = $" ";
            UlkeModel model = new UlkeModel();
            model.Baslik = "Güncelleme";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Güncelle";
            model.Type = "hidden";
            model.Ulke = db.Set<Ulke>().Find(id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Update(Ulke model)
        {
            //db.Unvans.Add(model);
            //db.Set<Unvan>().Add(model);
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            Session["Error"] = "Güncelleme Başarılı";
            Session["cls"] = "succsess";
            return RedirectToAction("List");
        }
    }
}