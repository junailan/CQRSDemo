using CommandService;
using CQRSDemo.Models;
using Model;
using QueryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CQRSDemo.Controllers
{
    public class UserController : Controller
    {
        private UserQueryService queryService = new UserQueryService();
        private UserComnandService commandService = new UserComnandService();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            commandService.RegisterUser(model.UserName, model.Password, model.UserName, model.Email, null, null, null, null, null, null);

            FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = queryService.GetUserByUserName(model.UserName);
                if (user != null && user.Password == model.Password)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "帐号或密码不正确，请检查！");
                }
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [Login]
        public ActionResult UserInfo()
        {
            UserInfoModel model = new UserInfoModel();
            model.UserInfo = queryService.GetUserByUserName(User.Identity.Name);
            model.BorrowRecords = queryService.GetBorrowRecordsByUserId(model.UserInfo.AggregateRootId);

            return View(model);
        }

        [Login]
        public ActionResult Edit()
        {
            User model = queryService.GetUserByUserName(User.Identity.Name);
            return View(model);
        }

        [Login]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (user.DisplayName == null || user.Email == null)
            {
                throw new Exception("昵称和邮箱不能为空");
            }
            commandService.UpdateUser(user.AggregateRootId, user.DisplayName, user.Email, user.ContactPhone, user.Address_Country, user.Address_State, user.Address_City, user.Address_Street, user.Address_Zip);
            return RedirectToAction("UserInfo");
        }

        [Login]
        public ActionResult BorrowBook(Guid bookId)
        {
            User user = queryService.GetUserByUserName(User.Identity.Name);
            commandService.BorrowBookToUser(user.AggregateRootId, bookId);
            return RedirectToAction("List", new { controller = "Book" });
        }

        [Login]
        public ActionResult ReturnBook(Guid bookId)
        {
            User user = queryService.GetUserByUserName(User.Identity.Name);
            commandService.ReturnBookFromUser(user.AggregateRootId, bookId);
            return RedirectToAction("UserInfo");
        }
    }
}