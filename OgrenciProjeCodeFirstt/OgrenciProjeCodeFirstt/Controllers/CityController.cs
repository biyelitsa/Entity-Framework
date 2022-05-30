using OgrenciProjeCodeFirstt.Models;
using OgrenciProjeCodeFirstt.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace OgrenciProjeCodeFirstt.Controllers
{
    public class CityController:Controller
    {
        OgrenciContext db = new OgrenciContext();
        public ActionResult List()
        {
            var slist = db.Set<City>().Select(x => new CityDTO
            {
                CityName = x.CityName,
                CityId = x.CityId
            }).ToList();
            return View("List", slist);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Session["Error"] = $" ";
            CityModel model = new CityModel();
            model.Baslik = "Yeni Giriş";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Kaydet";
            model.Type = "hidden";
            model.City = new City();
            model.Type = "hidden";
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Create(City model)
        {
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Session["Error"] = $" ";
            CityModel model = new CityModel();
            model.Baslik = "Güncelleme";
            model.BtnClass = "btn btn-success";
            model.BtnVal = "Güncelle";
            model.Type = "hidden";
            model.City = db.Set<City>().Find(id);
            //model.TeacherList = GetTeacherList();
            //model.CityList = GetCityList();
            model.Type = "hidden";
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Update(CityModel model)
        {
            City p = db.Set<City>().Find(model.City.CityId);

            db.Entry(model.City).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Session["Error"] = $" ";
            CityModel model = new CityModel();
            model.Baslik = "Silme";
            model.BtnClass = "btn btn-danger";
            model.BtnVal = "Sil";
            model.Type = "hidden";
            model.City = db.Set<City>().Find(id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Delete(City model)
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

                Session["Error"] = $"{model.CityId} kaydının bağlantısı bulunmaktadır. Silinemez.";
                Session["cls"] = "danger";
            }
            return RedirectToAction("List");

        }
    }
}