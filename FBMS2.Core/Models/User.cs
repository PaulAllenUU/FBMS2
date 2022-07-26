using System;
namespace FBMS2.Core.Models
{
    // Add User roles relevant to your application
    public enum Role { admin, manager, guest }
    
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // User role within application
        public Role Role { get; set; }

        //each user has a list of stock they can add, edit view and manage
        //always generate the list even if it is empty 
        public List<Stock> Stock { get; set; } = new List<Stock> ();

    }
}
