using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// import the Models (representing structure of tables in database)
using FBMS2.Core.Models; 

namespace FBMS2.Data.Repositories
{
    // The Context is How EntityFramework communicates with the database
    // We define DbSet properties for each table in the database
    public class DatabaseContext :DbContext
    {
         //authentication for the users
        public DbSet<User> Users { get; set; }

        //add Db context for Stock 
        public DbSet<Stock> Stock { get; set; }

        // Configure the context to use Specified database. We are using 
        // Sqlite database as it does not require any additional installations.
        // FBMS2 configured to allow use of MySql and Postgres
        // ideally connections strings should be stored in appsettings.json
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder                  
                .UseSqlite("Filename=data.db")
                //.UseMySQL("server=localhost; port=3306; database=xxx; user=xxx; password=xxx")
                //.UseNpgsql("host=localhost; port=5432; database=xxx; username=xxx; password=xxx")
                .LogTo(Console.WriteLine, LogLevel.Information) // remove in production
                .EnableSensitiveDataLogging()                   // remove in production
                ;
        }

        // Convenience method to recreate the database thus ensuring the new database takes 
        // account of any changes to Models or DatabaseContext. ONLY to be used in development
        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}
