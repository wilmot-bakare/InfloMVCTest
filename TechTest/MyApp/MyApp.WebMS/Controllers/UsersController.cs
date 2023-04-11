using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using MyApp.Models;
using MyApp.Services.Factories.Interfaces;
using MyApp.WebMS.Controllers.Base;
using MyApp.WebMS.Models;
using Newtonsoft.Json;
using Serilog;

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
            try
            {
                Log.Debug("Retrieving user list");
                var items = ServiceFactory.UserService.GetAll();
                var model = Mapper.Map<UserListViewModel>(items);
                return View("List", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving user list");
                return RedirectToAction("List");
            }
        }

        [Route("ActiveUserList", Name = "ActiveUserList")]
        public ActionResult ActiveUserList()
        {
            try
            {
                Log.Debug("Retrieving active user list");
                var items = ServiceFactory.UserService.FilterByActive();
                var model = Mapper.Map<UserListViewModel>(items);
                return View("List", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving user list");
                return RedirectToAction("List");
            }
        }

        [Route("InActiveUserList", Name = "InActiveUserList")]
        public ActionResult InActiveUserList()
        {
            try
            {
                Log.Debug("Retrieving inactive user list");
                var items = ServiceFactory.UserService.FilterByInActive();
                var model = Mapper.Map<UserListViewModel>(items);
                return View("List", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving user list");
                return RedirectToAction("List");
            }
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
                var req = JsonConvert.SerializeObject(model);
                Log.Debug("Creating user:"+ req);
                var user = Mapper.Map<User>(model);
                ServiceFactory.UserService.CreateUser(user);
                ViewBag.Message = "User Created Successfully";
               
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error retrieving user list");
                ViewBag.Message = "An Error occured. Please try again";
            }
           
            ModelState.Clear();
            return View("CreateUser");
        }


        [HttpGet]
        [Route("EditUser", Name = "EditUser")]
        public async Task<ActionResult> EditUser(int id)
        {
            try
            {
               
                Log.Debug("Editing user with ID:"+ id);
                var res = ServiceFactory.UserService.GetById(id);
                var model = Mapper.Map<UserListItemViewModel>(res);
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error editing user with Id:" + id);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [Route("SaveEditedUser", Name = "SaveEditedUser")]
        public async Task<ActionResult> SaveEditedUser(UserListItemViewModel model)
        {
            try
            {
                var req = JsonConvert.SerializeObject(model);
                Log.Debug("SaveEditedUser user:"+ req);
                var user = Mapper.Map<User>(model);
                var res = await ServiceFactory.UserService.Update(user);
                if (res != null)
                {
                    ViewBag.Message = "User Created Successfully";
                }

                Activity activity = new Activity
                {
                    Name = "SaveEditedUser",
                    Description = "User was edited",
                    UserId = user.Id
                };
                await ServiceFactory.UserService.AddActivity(activity);

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error saving edited user");
                return RedirectToAction("List");
            }


          
        }


        [Route("DeleteUser", Name = "DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                ServiceFactory.UserService.DeleteByID(id);
                ViewBag.Messsage = "Record Delete Successfully";
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting user");
                return RedirectToAction("List");
            }
        }

        [HttpGet]
        [Route("Details", Name = "Details")]
        public ActionResult Details(int id)
        {
            try
            {
                var user = ServiceFactory.UserService.GetById(id);
                var activities = user.Activities;
                var viewModel = new UserActivityViewModel
                {
                    Id = user.Id,
                    Forename = user.Forename,
                    Surname = user.Surname,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    DateOfBirth = user.DateOfBirth,
                    Activities = Mapper.Map<List<ActivityViewModel>>(activities)
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving user details");
                return RedirectToAction("List");
            }
        }
    }
}