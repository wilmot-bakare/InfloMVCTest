using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using MyApp.Models;
using MyApp.Services.Factories.Interfaces;
using MyApp.WebMS.Controllers.Base;
using MyApp.WebMS.Models;

namespace MyApp.WebMS.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : BaseController
    {
        public UsersController(IServiceFactory serviceFactory) : base(serviceFactory) {
        
        
        
        }

        [Route("", Name = "UserList")]
        public ActionResult List()
        {
            var items = ServiceFactory.UserService.GetAll();
            var model = Mapper.Map<UserListViewModel>(items);
            return View("List", model);
        }

        [Route("ActiveUserList", Name = "ActiveUserList")]
        public ActionResult ActiveUserList()
        {
            var items = ServiceFactory.UserService.FilterByActive();
            var model = Mapper.Map<UserListViewModel>(items);
            return View("List", model);
        }

        [Route("InActiveUserList", Name = "InActiveUserList")]
        public ActionResult InActiveUserList()
        {
            var items = ServiceFactory.UserService.FilterByInActive();
            var model = Mapper.Map<UserListViewModel>(items);
            return View("List", model);
        }

        [Route("CreateUser", Name = "CreateUser")]
        public ActionResult CreateUser()
        {
            return View("CreateUser");
        }

        [HttpPost]
        [Route("AddUser", Name = "AddUser")]
        public ActionResult AddUser(UserListItemViewModel model)
        {
            try
            {
                var user = Mapper.Map<User>(model);
                ServiceFactory.UserService.CreateUser(user);
                ViewBag.Message = "User Created Successfully";
            }
            catch(Exception ex)
            {
                ViewBag.Message = "An Error occured. Please try again";
            }
           
            ModelState.Clear();
            return View("CreateUser");
        }


        [HttpGet]
        [Route("EditUser", Name = "EditUser")]
        public async Task<ActionResult> EditUser(int id)
        {
            var res = ServiceFactory.UserService.GetById( id);
            var model = Mapper.Map<UserListItemViewModel>(res);
            return View(model);
        }

        [HttpPost]
        [Route("SaveEditedUser", Name = "SaveEditedUser")]
        public async Task<ActionResult> SaveEditedUser(UserListItemViewModel model)
        {
            var user = Mapper.Map<User>(model);
            var res = await ServiceFactory.UserService.Update(user);
            if (res != null)
            {
                ViewBag.Message = "User Created Successfully";
            }
            return RedirectToAction("List");
        }


        [Route("DeleteUser", Name = "DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            ServiceFactory.UserService.DeleteByID(id);
            ViewBag.Messsage = "Record Delete Successfully";
            return RedirectToAction("List");
        }
    }
}