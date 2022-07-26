
using FBMS2.Core.Models;
using FBMS2.Core.Services;
using FBMS2.Core.Security;
using FBMS2.Data.Repositories;
namespace FBMS2.Data.Services
{
    public class UserServiceDb : IUserService
    {
        private readonly DatabaseContext  ctx;

        public UserServiceDb()
        {
            ctx = new DatabaseContext(); 
        }

        public void Initialise()
        {
           ctx.Initialise(); 
        }

        // ------------------ Begin User Management Operations

        // retrieve list of Users
        public IList<User> GetUsers()
        {
            return ctx.Users.ToList();
        }

        // Retrive User by Id 
        public User GetUserById(int id)
        {
            return ctx.Users.FirstOrDefault(s => s.Id == id);
        }

        // Add a new User checking a User with same email does not exist
        public User AddUser(string firstName, string secondName, string email, string password, Role role)
        {     
            var existing = GetUserByEmail(email);
            if (existing != null)
            {
                return null;
            } 

            var user = new User
            {            
                FirstName = firstName,
                SecondName = secondName,
                Email = email,
                Password = Hasher.CalculateHash(password), // can hash if required 
                Role = role              
            };
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user; // return newly added User
        }

        // Delete the User identified by Id returning true if deleted and false if not found
        public bool DeleteUser(int id)
        {
            var s = GetUserById(id);
            if (s == null)
            {
                return false;
            }
            ctx.Users.Remove(s);
            ctx.SaveChanges();
            return true;
        }

        // Update the User with the details in updated 
        public User UpdateUser(User updated)
        {
            // verify the User exists
            var User = GetUserById(updated.Id);
            if (User == null)
            {
                return null;
            }
            // verify email address is registered or available to this user
            if (!IsEmailAvailable(updated.Email, updated.Id))
            {
                return null;
            }
            // update the details of the User retrieved and save
            User.FirstName = updated.FirstName;
            User.SecondName = updated.SecondName;
            User.Email = updated.Email;
            User.Password = Hasher.CalculateHash(updated.Password);  
            User.Role = updated.Role; 

            ctx.SaveChanges();          
            return User;
        }

        // Find a user with specified email address
        public User GetUserByEmail(string email)
        {
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }

        // Verify if email is available or registered to specified user
        public bool IsEmailAvailable(string email, int userId)
        {
            return ctx.Users.FirstOrDefault(u => u.Email == email && u.Id != userId) == null;
        }

        public IList<User> GetUsersQuery(Func<User,bool> q)
        {
            return ctx.Users.Where(q).ToList();
        }

        public User Authenticate(string email, string password)
        {
            // retrieve the user based on the EmailAddress (assumes EmailAddress is unique)
            var user = GetUserByEmail(email);

            // Verify the user exists and Hashed User password matches the password provided
            return (user != null && Hasher.ValidateHash(user.Password, password)) ? user : null;
            //return (user != null && user.Password == password ) ? user: null;
        }

        public IList<Stock> GetAllStock()
        {
            return ctx.Stock.ToList();
        }

        public Stock GetStockById(int id)
        {
            return ctx.Stock.FirstOrDefault( x => x.Id == id);
            
        }

        public Stock GetStockByDescription(string description)
        {
            return ctx.Stock.FirstOrDefault ( x => x.Description.Equals(description));
        }

        public Stock GetStockByExpiryDate(DateTime expiryDate)
        {
            return ctx.Stock.FirstOrDefault ( x => x.ExpiryDate == expiryDate);
        }

        public Stock AddStock(int userId, string description, int quantity, DateTime expiryDate)
        {
            //get the user who is currently logged in and adding the stock item
            var user = GetUserById(userId);

            //iof they do not exist then (not logged in) then return null
            if(user == null) return null;

            //create the new stock object and add all properties
            var stock = new Stock
            {
                UserId = userId,
                Description = description,
                Quantity = quantity,
                ExpiryDate = expiryDate,
            };

            //add the newly created stock object to the database
            ctx.Stock.Add(stock);
            //save the changes
            ctx.SaveChanges();
            //return the newly created stock object
            return stock;
        
        }

        public Stock UpdateStock(Stock updated)
        {
            //check that the item of stock exists before it can be updated
            var stock = GetStockById(updated.Id);

            //if it does not exist then cannot updated so return null
            if(stock == null)
            {
                return null;
            }

            //update all properties with the properties passed
            stock.Description = updated.Description;
            stock.Quantity = updated.Quantity;
            stock.ExpiryDate = updated.ExpiryDate;

            //save the changes to the database
            ctx.SaveChanges();

            //return the object that has just been updated
            return stock;
        }

        public bool DeleteStockById(int id)
        {
            //check that the item of stock to be deleted exists within the database
            var stock = GetStockById(id);

            //if this variable is null then cannot be deleted so return false
            if (stock == null) return false;

            //otherwise remove the stock from the database
            ctx.Stock.Remove(stock);

            //save the changes and return true
            ctx.SaveChanges();
            return true;
        }
    }
}