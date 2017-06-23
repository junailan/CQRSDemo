using CommandService;
using Model;
using QueryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CQRSDemo.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View("List");
        }

        private BookQueryService queryService = new BookQueryService();
        private BookCommandService comnandService = new BookCommandService();

        public ActionResult List()
        {
            List<Book> books = queryService.GetBooks();
            return View(books);
        }

        public ActionResult Detail(Guid id)
        {
            Book book = queryService.GetBookById(id);

            return View(book);
        }

        [IsAdmin]
        public ActionResult Create()
        {
            return View();
        }

        [IsAdmin]
        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (book.Title == null || book.Author == null || book.Description == null || book.ISBN == null)
            {
                throw new Exception("书名|作者|描述|ISBN不能为空");
            }
            comnandService.AddBook(book.Title, book.Author, book.Description, book.ISBN, book.Pages, book.Inventory);
            return RedirectToAction("List");
        }

        [IsAdmin]
        public ActionResult Edit(Guid id)
        {
            Book book = queryService.GetBookById(id);
            return View(book);
        }

        [IsAdmin]
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (book.Title == null || book.Author == null || book.Description == null || book.ISBN == null)
            {
                throw new Exception("书名|作者|描述|ISBN不能为空");
            }
            comnandService.UpdateBook(book.AggregateRootId, book.Title, book.Author, book.Description, book.ISBN, book.Pages, book.Inventory);
            return RedirectToAction("List");
        }
    }
}