
using FBMS2.Core.Models;

namespace FBMS2.Core.Services
{
    // This interface describes the operations that a UserService class implementation should provide
    public interface IUserService
    {
        // Initialise the repository - only to be used during development 
        void Initialise();

        // ---------------- User Management --------------

        //display a list of users who have been authenticated
        IList<User> GetUsers();

        //get a specific user by their Id
        User GetUserById (int id);

        //get a specific user by their e mail address
        User GetUserByEmail(string email);

        //check that e mail address is not being used by another user
        bool IsEmailAvailable(string email, int userId);

        //add another using - firstname, secondname, e mail, password and role passed as parameters
        User AddUser(string firstName, string secondName, string email, string password, Role role);

        //update existing user - pass the properties of the updated user in as parameter
        User UpdateUser(User updated);

        //delete an existing user by their id
        bool DeleteUser(int id);

        //authenticate a user based on their e mail address and password
        User Authenticate(string email, string password);

        //-----------End of User Mananagement methods-----

        //---------Begin Stock Management Methods---------//

        //get list of all of the stock available in the database currently
        IList <Stock> GetAllStock();

        //get stock by id - id passed in as parameter from the user
        Stock GetStockById(int id);

        //get stock by descrption passed in from the user
        Stock GetStockByDescription(string description);

        //get stock by expiry date
        Stock GetStockByExpiryDate(DateTime expiryDate);

        //add stock using all properties
        Stock AddStock(int userId, string description, int quantity, DateTime expiryDate); 

        //update existing stock
        Stock UpdateStock(Stock updated);

        //delete an item of stock using id passed in as a parameter
        bool DeleteStockById(int id);
       
    }
    
}
