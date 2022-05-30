using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web.DynamicData;

namespace DbCodeFirst.Data
{
    public class AddContext : DbContext
    {
        

        // Your context has been configured to use a 'AddContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DbCodeFirst.Data.AddContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AddContext' 
        // connection string in the application configuration file.
        public AddContext()
            : base("Baglanti")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Personel> Personel { get; set; }
    }
    [TableName("Personel")]
    public class Personel
    {
        [Key]
        public int PersonelId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}