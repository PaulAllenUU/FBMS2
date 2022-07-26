
using Xunit;
using FBMS2.Core.Models;
using FBMS2.Core.Services;


using FBMS2.Data.Services;

namespace FBMS2.Test
{
    public class ServiceTests
    {
        private IUserService service;

        public ServiceTests()
        {
            service = new UserServiceDb();
            service.Initialise();
        }

        [Fact]
        public void EmptyDbShouldReturnNoUsers()
        {
            // act
            var users = service.GetUsers();

            // assert
            Assert.Equal(0, users.Count);
        }
        
        [Fact]
        public void AddingUsersShouldWork()
        {
            // arrange
            service.AddUser("Paul", "Allen", "admin@mail.com", "admin", Role.admin);
            service.AddUser("Steven", "Martin", "manager@mail.com", "manager", Role.manager);

            // act
            var users = service.GetUsers();

            // assert
            Assert.Equal(2, users.Count);
        }


        [Fact]
        public void User_WhenAddingDuplicateEmail_ShouldReturnNull()
        {
            var user = service.AddUser("Steven", "Martin", "manager@mail.com", "manager", Role.manager);

            var user2 = service.AddUser("Trevor", "Gillen", "manager@mail.com", "manager", Role.manager);

            //user should be added but user2 should not because it is the same e mail address
            Assert.NotNull(user);
            Assert.Null(user2);
        }

        [Fact]
        public void User_AddAllPropertiesShouldWork()
        {
            //arrange
            var user = service.AddUser("Steven", "Martin", "manager@mail.com", "manager", Role.manager);

            //assert - equals method can only test properties with string data type
            Assert.True("manager@mail.com".Equals(user.Email));
            Assert.True("Steven".Equals(user.FirstName));
            Assert.True("Martin".Equals(user.SecondName));
        }

        [Fact]
        public void User_GetAllUsersWhenNone_ShouldReturnNone()
        {
            var users = service.GetUsers();
            var count = users.Count;

            Assert.Equal(0, count);
        }

        [Fact]
        public void User_DeleteUserShouldDecrementUserCount()
        {
            //arrange
            var user = service.AddUser("Steven", "Martin", "manager@mail.com", "manager", Role.manager);

            var removed = service.DeleteUser(1);

            //act
            var users = service.GetUsers();
            var count = users.Count;

            Assert.Equal(0, count);
    
        }

        [Fact]
        public void UpdatingUserShouldWork()
        {
            // arrange
            var user = service.AddUser("Steven", "Martin", "manager@mail.com", "manager", Role.manager);
            
            // act
            user.FirstName = "Mr";
            user.SecondName = "Administrator";            
            var updatedUser = service.UpdateUser(user);

            // assert
            Assert.Equal("Administrator", user.FirstName);
            Assert.Equal("@mail.com", user.Email);
        }

        [Fact]
        public void LoginWithValidCredentialsShouldWork()
        {
            // arrange
            var login = service.AddUser("Steven", "Martin", "manager@mail.com", "manager", Role.manager);
            
            // act            
            var user = service.Authenticate("admin@mail.com","manager");

            // assert
            Assert.NotNull(user);
           
        }

        [Fact]
        public void LoginWithInvalidCredentialsShouldNotWork()
        {
            // arrange
            var login = service.AddUser("Steven", "Martin", "manager@mail.com", "manager", Role.manager);

            // act      
            var user = service.Authenticate("admin@mail.com","xxx");

            // assert
            Assert.Null(user);
           
        }

    }
}
