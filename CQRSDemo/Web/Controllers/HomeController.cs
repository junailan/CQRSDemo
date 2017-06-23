using CommandService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CQRSDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserComnandService userService = new UserComnandService();
            //userService.RegisterUser("taylorchen", "123456", "赎罪", "taylorchen@tencent.com", "15099907289", "中国", "State", "Street", "ShenZhen", "518000");

            //userService.UpdateUser(new Guid("42991D99-CDAA-45EA-9C9C-C101DCEA3051"), "赎罪_test", "taylorchen@tencent.com", "15099907289", "中国", "State", "Street", "ShenZhen", "518000");


            BookCommandService bookService = new BookCommandService();
            //bookService.AddBook("人类简史", "无", "无", "无", 150, 10);

            //bookService.UpdateBook(new Guid("F6151D14-3EF4-497A-8EBF-6D3856604E1B"), "测试书籍", "无", "无", "无", 150, 10);

            //userService.BorrowBookToUser(new Guid("42991D99-CDAA-45EA-9C9C-C101DCEA3051"), new Guid("F6151D14-3EF4-497A-8EBF-6D3856604E1B"));

            //userService.ReturnBookFromUser(new Guid("42991D99-CDAA-45EA-9C9C-C101DCEA3051"), new Guid("F6151D14-3EF4-497A-8EBF-6D3856604E1B"));

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NoAuthentication()
        {
            return View();
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}