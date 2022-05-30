using OgrenciProjeCodeFirstt.Models;
using OgrenciProjeCodeFirstt.Models.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciProjeCodeFirstt.Controllers
{
    public class StudentController:Controller
    {
        //public ActionResult List()
        //{
        //    var pList = db.Set<Personel>().Select(x => new PersonelDTO
        //    {
        //        Ad = x.Ad,
        //        Ikamet = x.Ulke.UlkeAd,
        //        Id = x.PersonelId,
        //        Soyad = x.Soyad,
        //        UnvanAd = x.Unvan.UnvanAd,
        //        Yonetici = x.Personel2.Ad + " " + x.Personel2.Soyad,
        //        UnvanId = x.UnvanId ?? 0
        //    }).Distinct().ToList();
        //    return View("List", pList);
        //}

        OgrenciContext db = new OgrenciContext();
        public ActionResult List()
        {
            var slist = db.Set<Student>().Select(x => new StudentDTO
            {
                Name = x.Name,
                Surname = x.Surname,
                CityId = x.CityId,
                MotherName = x.MotherName,
            }).ToList();
            return View("List", slist);
        }
        private List<CityDTO> GetCityList()
        {
            return db.Set<City>().Select(x => new CityDTO
            {
                CityId = x.CityId,
                CityName = x.CityName
            }).ToList();
        }

        private List<TeacherDTO> GetTeacherList()
        {
            return db.Set<Teacher>().Select(x => new TeacherDTO
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                CityId = x.CityId,
                MotherName = x.MotherName,
                Salary= x.Salary
            }).Distinct().ToList();
        }

        [HttpGet]
        public ActionResult Create()
        {
            Session["Error"] = $" ";
            StudentModel model = new StudentModel();
            model.Baslik = "Yeni Giriş";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Kaydet";
            model.Type = "hidden";
            model.Student = new Student();
            model.TeacherList = GetTeacherList();
            model.CityList = GetCityList();
            model.Type = "hidden";
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Create(StudentModel model)
        {
            db.Entry(model.Student).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Session["Error"] = $" ";
            StudentModel model = new StudentModel();
            model.Baslik = "Güncelleme";
            model.BtnClass = "btn btn-success";
            model.BtnVal = "Güncelle";
            model.Type = "hidden";
            model.Student = db.Set<Student>().Find(id);
            model.TeacherList = GetTeacherList();
            model.CityList = GetCityList();
            model.Type = "hidden";
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Update(StudentModel model)
        {
            Student p = db.Set<Student>().Find(model.Student.Id);

            db.Entry(model.Student).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Session["Error"] = $" ";
            StudentModel model = new StudentModel();
            model.Baslik = "Silme";
            model.BtnClass = "btn btn-danger";
            model.BtnVal = "Sil";
            model.Type = "hidden";
            model.Student = db.Set<Student>().Find(id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Delete(Student model)
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

                Session["Error"] = $"{model.Id} kaydının bağlantısı bulunmaktadır. Silinemez.";
                Session["cls"] = "danger";
            }
            return RedirectToAction("List");

        }


    }
}