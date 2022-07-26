
using FBMS2.Core.Models;
using FBMS2.Core.Services;

namespace FBMS2.Data.Services
{
    public static class Seeder
    {
        // use this class to seed the database with dummy 
        // test data using an IUserService 
         public static void Seed(IUserService svc)
        {
            svc.Initialise();

            // add users
            svc.AddUser("Paul", "Allen", "admin@mail.com", "admin", Role.admin);
            svc.AddUser("Steven", "Martin", "manager@mail.com", "manager", Role.manager);
            svc.AddUser("Peter", "Gillen", "guest@mail.com", "guest", Role.guest);    
        }
    }
}
