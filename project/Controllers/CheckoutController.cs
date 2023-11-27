using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models;
using project.Models.DBObjects;

namespace project.Controllers
{
    public class CheckoutController : Controller
    {
        private Repository.CheckoutRepository _repository;
        private Repository.BookRepository _repository_book;
        private Repository.MemberRepository _repository_member;

        public CheckoutController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.CheckoutRepository(dbContext);
            _repository_book = new Repository.BookRepository(dbContext);
            _repository_member = new Repository.MemberRepository(dbContext);
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
            model = _repository.UpdateBookName(model);
            model = _repository.UpdateMemberName(model);

            return View("CheckoutDetails", model);
        }

        // GET: CheckoutController/Create
        public ActionResult Create()
        {
            var book = _repository_book.GetAllBooks();
            var member = _repository_member.GetAllMembers();

            var model = new Models.CheckoutModel
            {
                BookList = book,
                MemberList = member
            };

            return View("CheckoutCreate", model);
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

                model = _repository.UpdateMemberName(model);
                model = _repository.UpdateBookName(model);

                var book = _repository_book.GetAllBooks();
                var member = _repository_member.GetAllMembers();
                model.BookList = book;
                model.MemberList = member;

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

            var book = _repository_book.GetAllBooks();
            var member = _repository_member.GetAllMembers();

            model.BookList = book;
            model.MemberList = member;

            return View("CheckoutEdit", model);
        }

        // POST: CheckoutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, CheckoutModel model)
        {
            try
            {
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateCheckout(model);
                    return RedirectToAction("Index");
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
            model = _repository.UpdateBookName(model);
            model = _repository.UpdateMemberName(model);

            return View("CheckoutDelete", model);
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
