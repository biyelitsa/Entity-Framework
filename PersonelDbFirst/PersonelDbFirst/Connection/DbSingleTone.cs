using PersonelDbFirst.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelDbFirst.Connection
{
    public static class DbSingleTone
    {
        static perdb3Entities db;
        public static perdb3Entities GetConnection()
        {
            if (db == null)
            {
                db = new perdb3Entities();
                
            }
            return db;
        }
    }
}