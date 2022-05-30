using OgrenciProjeCodeFirstt.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgrenciProjeCodeFirstt.Models
{
    public class StudentModel
    {
        public Student Student { get; set; }
        public string Baslik { get; set; }
        public string BtnVal { get; set; }
        public string BtnClass { get; set; }
        public string Type { get; set; }
        public List<TeacherDTO> TeacherList { get; set; }
        public List<CityDTO> CityList { get; set; }

    }
}