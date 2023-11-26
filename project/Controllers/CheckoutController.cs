using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class CheckoutController : Controller
    {
        private Repository.CheckoutRepository _repository;

        public CheckoutController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.CheckoutRepository(dbContext);
        }

        // GET: CheckoutController
        public ActionResult Index()
        {
            var index = _repository.GetAllCheckouts();
            return View(index);
        }

        // GET: CheckoutController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetCheckoutModel(id);
            return View("CheckoutDetails", model);
        }

        // GET: CheckoutController/Create
        public ActionResult Create()
        {
            return View("CheckoutCreate");
        }

        // POST: CheckoutController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.CheckoutModel model = new Models.CheckoutModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertCheckout(model);
                }
                return View("CheckoutDetails", model);

            }
            catch
            {
                return View("CheckoutCreate");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CheckoutController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetCheckoutModel(id);
            return View("CheckoutEdit", model);
        }

        // POST: CheckoutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new CheckoutModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateCheckout(model);
                    return RedirectToAction("CheckoutDetails", model);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: CheckoutController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetCheckoutModel(id);
            return View("CheckoutDelete");
        }

        // POST: CheckoutController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _repository.DeleteCheckout(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CheckoutDelete", id);
            }
        }
    }
}
