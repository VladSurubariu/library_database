using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class GenreController : Controller
    {
        private Repository.GenreRepository _repository;

        public GenreController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.GenreRepository(dbContext);
        }

        // GET: GenreController
        public ActionResult Index()
        {
            var genre = _repository.GetAllGenres();
            return View("Index", genre);
        }

        // GET: GenreController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetGenreModel(id);
            return View("GenreDetails", model);
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View("GenreCreate");
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.GenreModel model = new Models.GenreModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertGenre(model);
                }
                return View("GenreDetails", model);;

            }
            catch
            {
                return View("Index");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: GenreController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetGenreModel(id);
            return View("GenreEdit", model);
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new GenreModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateGenre(model);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index", id);
            }

            return RedirectToAction("Index", id);
        }

        // GET: GenreController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetGenreModel(id);
            return View("GenreDelete", model);
        }

        // POST: GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _repository.DeleteGenre(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("GenreDelete", id);
            }
        }
    }
}
