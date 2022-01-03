using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroVistaMVC.DatabaseContext;
using MicroVistaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroVistaMVC.Controllers
{


    [TypeFilter(typeof(FiltersController))]
    public class AdminController : Controller
    {
        private readonly DBContextClass _dal;


        public AdminController(DBContextClass dal)
        {
            _dal = dal;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View(_dal.studentTable.ToList());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(500);
            }
         StudentClass user = _dal.studentTable.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }



        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentClass user)
        {
            if (ModelState.IsValid)
            {
                _dal.studentTable.Add(user);
                await _dal.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }




        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(500);
            }
            StudentClass user = _dal.studentTable.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentClass user)
        {
            if (ModelState.IsValid)
            {
                _dal.Entry(user).State = EntityState.Modified;
                _dal.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(500);
            }
            StudentClass inventory = _dal.studentTable.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentClass user = _dal.studentTable.Find(id);
            _dal.studentTable.Remove(user);
            _dal.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dal.Dispose();
            }
            base.Dispose(disposing);
        }




    }
}
