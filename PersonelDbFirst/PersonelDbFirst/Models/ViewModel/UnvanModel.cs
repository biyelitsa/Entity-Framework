﻿using PersonelDbFirst.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelDbFirst.Models.ViewModel
{
    public class UnvanModel
    {
        public Unvan Unvan { get; set; }
        public string Baslik { get; set; }
        public string BtnVal { get; set; }
        public string BtnClass { get; set; }
        public string Type { get; set; }
    }
}