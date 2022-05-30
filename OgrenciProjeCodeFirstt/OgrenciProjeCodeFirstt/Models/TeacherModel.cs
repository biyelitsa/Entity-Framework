using OgrenciProjeCodeFirstt.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgrenciProjeCodeFirstt.Models
{
    public class TeacherModel
    {
        public Teacher Teacher { get; set; }
        public string Baslik { get; set; }
        public string BtnVal { get; set; }
        public string BtnClass { get; set; }
        public string Type { get; set; }
        public List<CityModel> CityList { get; set; }
    }
}