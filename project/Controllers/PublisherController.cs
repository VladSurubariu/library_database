using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class PublisherController : Controller
    {
        private Repository.PublisherRepository _repository;

        public PublisherController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.PublisherRepository(dbContext);
        }

        // GET: PublisherController
        public ActionResult Index()
        {
            var publisher = _repository.GetAllPublishers();
            return View("Index", publisher);
        }

        // GET: PublisherController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetPublisherModel(id);
            return View("PublisherDetails", model);
        }

        // GET: PublisherController/Create
        public ActionResult Create()
        {
            return View("PublisherCreate");
        }

        // POST: PublisherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.PublisherModel model = new Models.PublisherModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertPublisher(model);
                }
                return View("PublisherDetails", model);

            }
            catch
            {
                return View("Index");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: PublisherController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetPublisherModel(id);
            return View("PublisherEdit", model);
        }

        // POST: PublisherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new PublisherModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdatePublisher(model);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index", id);
            }

            return RedirectToAction("Index", id);
        }

        // GET: PublisherController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetPublisherModel(id);
            return View("PublisherDelete");
        }

        // POST: PublisherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _repository.DeletePublisher(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("PublisherDelete", id);
            }
        }
    }
}
