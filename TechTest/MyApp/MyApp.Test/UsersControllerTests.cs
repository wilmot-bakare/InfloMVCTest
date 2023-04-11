using NUnit.Framework;
using System.Linq;
using System.Web.Mvc;
using MyApp.Services;
using MyApp.Services.Factories.Interfaces;
using MyApp.WebMS.Controllers;
using MyApp.WebMS.Models;
using Moq;
using MyApp.Services.Domain.Interfaces;
using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Test
{
    public class UsersControllerTests
    {
        private Mock<IServiceFactory> _serviceFactoryMock;
        private Mock<IUserService> _userServiceMock;
        private UsersController _controller;
        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _serviceFactoryMock = new Mock<IServiceFactory>();
            _serviceFactoryMock.Setup(x => x.UserService).Returns(_userServiceMock.Object);
            _controller = new UsersController(_serviceFactoryMock.Object);
        }

        [Test]
        public void UserList_ReturnsViewWithModel()
        {
            // Arrange
            var userList = new List<User>
            {
                new User {Forename = "Will", Surname = "Bakar", Email = "will.Bakar@example.com", IsActive = true},
                new User {Forename = "Bill", Surname = "Nagari", Email = "Bill.Nagari@example.com", IsActive = false}
            };
            _userServiceMock.Setup(x => x.GetAll()).Returns(userList);

            // Act
            var result = _controller.List() as ViewResult;
            var model = result.Model as UserListViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("List", result.ViewName);
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Items.Count());
        }

        [Test]
        public void ActiveUserList_ReturnsViewWithModel()
        {
            // Arrange
            var userList = new List<User>
            {
                new User {Forename = "Will", Surname = "Bakar", Email = "will.Bakar@example.com", IsActive = true},
                new User {Forename = "Bill", Surname = "Nagari", Email = "Bill.Nagari@example.com", IsActive = false}
            };
            _userServiceMock.Setup(x => x.FilterByActive()).Returns(userList.Where(x => x.IsActive));

            // Act
            var result = _controller.ActiveUserList() as ViewResult;
            var model = result.Model as UserListViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("List", result.ViewName);
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Items.Count());
            Assert.AreEqual("Will", model.Items.First().Forename);
        }

        [Test]
        public void ActiveUserList_ReturnsOnlyActiveUsers()
        {
            // Arrange
            var userList = new List<User>
            {
                new User {Forename = "Will", Surname = "Bakar", Email = "will.Bakar@example.com", IsActive = true},
                new User {Forename = "Bill", Surname = "Nagari", Email = "Bill.Nagari@example.com", IsActive = true},
                 new User {Forename = "Kell", Surname = "Nagari", Email = "Kell.Nagari@example.com", IsActive = false}
            };
            _userServiceMock.Setup(x => x.FilterByActive()).Returns(userList.Where(x => x.IsActive));

            // Act
            var result = _controller.ActiveUserList() as ViewResult;
            var model = result.Model as UserListViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("List", result.ViewName);
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Items.Count());
            Assert.IsTrue(model.Items.All(u => u.IsActive == true));
        }

        [Test]
        public void InActiveUserList_ReturnsViewWithModel()
        {
            // Arrange
            var userList = new List<User>
            {
                new User {Forename = "Will", Surname = "Bakar", Email = "will.Bakar@example.com", IsActive = false},
                new User {Forename = "Bill", Surname = "Nagari", Email = "Bill.Nagari@example.com", IsActive = false},
                new User {Forename = "Kell", Surname = "Nagari", Email = "Kell.Nagari@example.com", IsActive = true}

            };
            _userServiceMock.Setup(x => x.FilterByInActive()).Returns(userList.Where(x => !x.IsActive));

            // Act
            var result = _controller.InActiveUserList() as ViewResult;
            var model = result.Model as UserListViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("List", result.ViewName);
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Items.Count());
            Assert.AreEqual("Will", model.Items.First().Forename);
        }

        [Test]
        public void InActiveUserList_ReturnsOnlyInActiveUsers()
        {
            // Arrange
            var userList = new List<User>
            {
                new User {Forename = "Will", Surname = "Bakar", Email = "will.Bakar@example.com", IsActive = false},
                new User {Forename = "Bill", Surname = "Nagari", Email = "Bill.Nagari@example.com", IsActive = false},
                new User {Forename = "Kell", Surname = "Nagari", Email = "Kell.Nagari@example.com", IsActive = true}
            };
            _userServiceMock.Setup(x => x.FilterByInActive()).Returns(userList.Where(x => !x.IsActive));

            // Act
            var result = _controller.InActiveUserList() as ViewResult;
            var model = result.Model as UserListViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("List", result.ViewName);
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Items.Count());
            Assert.IsTrue(model.Items.All(u => u.IsActive == false));
        }
    }
}