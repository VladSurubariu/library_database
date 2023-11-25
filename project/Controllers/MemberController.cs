using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class MemberController : Controller
    {
        private Repository.MemberRepository _repository;

        public MemberController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.MemberRepository(dbContext);
        }

        public ActionResult Index()
        {
            var member = _repository.GetAllMembers();
            return View("Index", member);
        }

        // GET: MemberController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetMemberModel(id);
            return View("MemberDetails", model);
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            return View("MemberCreate");
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.MemberModel model = new Models.MemberModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertMember(model);
                }
                return View("MemberCreate");

            }
            catch
            {
                return View("MemberCreate");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetMemberModel(id);
            return View("MemberEdit", model);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new MemberModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateMember(model);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index", id);
            }

            return RedirectToAction("Index", id);
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetMemberModel(id);
            return View("MemberDelete");
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _repository.DeleteMember(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("MemberDelete", id);
            }
        }
    }
}
