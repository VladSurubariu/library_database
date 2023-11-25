using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class BookController : Controller
    {
        private Repository.BookRepository _repository;

        public BookController (ApplicationDbContext dbContext)
        {
            _repository = new Repository.BookRepository(dbContext);
        }

        // GET: BookController
        public ActionResult Index()
        {
            var books = _repository.GetAllBooks();
            return View("Index", books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetBookModel(id);
            return View("BookDetails", model);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View("BookCreate");
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.BookModel model = new Models.BookModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertBook(model);
                }
                return View("BookCreate");

            }
            catch
            {
                return View("BookCreate");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetBookModel(id);
            return View("BookEdit", model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new BookModel(); 
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateBook(model);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index", id);
            }

            return RedirectToAction("Index", id);
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetBookModel(id);
            return View("BookDelete");
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _repository.DeleteBook(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("BookDelete", id);
            }
        }
    }
}
