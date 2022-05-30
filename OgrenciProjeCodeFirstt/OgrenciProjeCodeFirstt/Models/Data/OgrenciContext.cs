using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace OgrenciProjeCodeFirstt.Models.Data
{
    public class OgrenciContext : DbContext
    {
        // Your context has been configured to use a 'OgrenciContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'OgrenciProjeCodeFirstt.Models.Data.OgrenciContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'OgrenciContext' 
        // connection string in the application configuration file.
        public OgrenciContext()
            : base("Baglanti")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<City> City { get; set; }
    }

    abstract public class Base
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CityId { get; set; }
        public string MotherName { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public string FullName()
        {
            return Name + " " + Surname;
        }
    }
    public class Student:Base
    {

    }
    public class Teacher:Base
    {
        public int Salary { get; set; }
    }
    public class City
    {
        public City()
        {
            this.Students = new HashSet<Student>();
            this.Teachers = new HashSet<Teacher>();
        }
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}