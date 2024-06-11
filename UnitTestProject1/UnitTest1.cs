using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DEprop;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private UserService userService;
        [TestInitialize]
        public void Setup()
        {
            // Инициализация сервиса перед каждым тестом
            userService = new UserService();
        }

        [TestMethod]
        public void CreateUser_ReturnsFalse()
        {
            // Arrange
            Users newUser = new Users();
            int userId = 1;
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (Users user1 in db.Users)
                {
                    if (user1.UserId == userId)
                    {
                        newUser = user1;
                    }
                }
            }

            // Act
            bool result = userService.CreateUser(newUser);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateUser_ReturnsFalse()
        {
            Users updatedUser = new Users();
            int userId = 1;
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach(Users user1 in db.Users)
                {
                    if(user1.UserId == userId)
                    {
                        updatedUser = user1;
                    }
                }
            }

            // Act
            bool result = userService.UpdateUser(updatedUser);

            // Assert
            Assert.IsTrue(result);
        }
    }

    public class UserService
    {
        private Dictionary<int, Users> users = new Dictionary<int, Users>();

        public bool CreateUser(Users user)
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                db.Users.Add(user);
                return true;
            }
        }

        public bool UpdateUser(Users user)
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                user.UserSurname = "КАИ";
                return true;
            }
        }
    }
}
