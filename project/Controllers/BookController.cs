using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class BookController : Controller
    {
        private Repository.BookRepository _repository;
        private Repository.GenreRepository _repository_genre;
        private Repository.PublisherRepository _repository_publisher;

        public BookController (ApplicationDbContext dbContext)
        {
            _repository = new Repository.BookRepository(dbContext);
            _repository_genre = new Repository.GenreRepository(dbContext);
            _repository_publisher = new Repository.PublisherRepository(dbContext);
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
            model = _repository.UpdateGenreName(model);
            model = _repository.UpdatePublisherName(model);

            return View("BookDetails", model);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var genre = _repository_genre.GetAllGenres();
            var publisher = _repository_publisher.GetAllPublishers();

            var model = new Models.BookModel
            {
                GenreList = genre,
                PublisherList = publisher
            };

            return View("BookCreate", model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookModel model)
        {
            try
            {

                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertBook(model);
                }

                model = _repository.UpdateGenreName(model);
                model = _repository.UpdatePublisherName(model);
                
                var genre = _repository_genre.GetAllGenres();
                var publisher = _repository_publisher.GetAllPublishers();

                model.GenreList = genre;
                model.PublisherList = publisher;


                return View("BookDetails", model);

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
            model = _repository.UpdateGenreName(model);
            model = _repository.UpdatePublisherName(model);
            return View("BookDelete", model);
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
