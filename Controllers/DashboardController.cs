using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroVistaMVC.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroVistaMVC.Controllers
{
    public class DashboardController : Controller
    {
        // GET: DashboardController

        private readonly DBContextClass _dal;

        public DashboardController(DBContextClass dal)
        {
            _dal = dal;
        }

        public ActionResult Index()
        {
            return View(_dal.studentTable.ToList());
        }

        // GET: DashboardController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashboardController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashboardController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
