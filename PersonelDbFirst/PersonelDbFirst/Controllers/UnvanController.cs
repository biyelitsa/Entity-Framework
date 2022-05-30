using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelDbFirst.Connection;
using PersonelDbFirst.Models.Data;
using PersonelDbFirst.Models.ViewModel;

namespace PersonelDbFirst.Controllers
{
    public class UnvanController : Controller
    {
        // GET: Unvan

        perdb3Entities db = DbSingleTone.GetConnection();

        public ActionResult List()
        {
            //var uList = db.Unvans.ToList();
            var uList = db.Set<Unvan>().ToList();
            var personeller = db.Set<Unvan>().FirstOrDefault().Personels;
            return View(uList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Session["Error"] = $" ";
            UnvanModel model = new UnvanModel();
            model.Baslik = "Yeni Giriş";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Kaydet";
            model.Type = "hidden";
            model.Unvan = new Unvan();
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Create(Unvan model)
        {
            //db.Unvans.Add(model);
            //db.Set<Unvan>().Add(model);
            db.Entry(model).State = EntityState.Added; 
            db.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Session["Error"] = $" ";
            UnvanModel model = new UnvanModel();
            model.Baslik = "Silme";
            model.BtnClass = "btn btn-danger";
            model.BtnVal = "Sil";
            model.Type = "hidden";
            model.Unvan = db.Set<Unvan>().Find(id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Delete(Unvan model)
        {
            try
            {
                Session["Error"] = $" ";
                //db.Unvans.Remove(model);
                //db.Set<Unvan>().Remove(model);
                db.Entry(model).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //ViewBag.Error = ex.Message;
                
                Session["Error"]= $"{model.UnvanId} kaydının bağlantısı bulunmaktadır. Silinemez.";
                Session["cls"] = "danger";
            }
            return RedirectToAction("List");

        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Session["Error"] = $" ";
            UnvanModel model = new UnvanModel();
            model.Baslik = "Güncelleme";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Güncelle";
            model.Type = "hidden";
            model.Unvan = db.Set<Unvan>().Find(id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Update(Unvan model)
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