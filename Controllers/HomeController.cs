using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroVistaMVC.DatabaseContext;
using MicroVistaMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace MicroVistaMVC.Controllers
{
    public class HomeController : Controller
    { 
        private readonly DBContextClass _dal;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(DBContextClass dal, IWebHostEnvironment environment)
        {
           _dal = dal;
            _hostingEnvironment = environment;
        }

         
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            //HttpContext.Session.Abandon();
            return RedirectToAction("Index");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
       // [Route("Registration")]
        public async Task<IActionResult> Registration(StudentClass user)
        {
 

            try
            {

                if (Request.Form["EUserRole"].ToString().Equals("1"))
                {
                    user.UserRole = "Student";
                }
                else
                {
                    user.UserRole = "Admin";
                }



                if (ModelState.IsValid)
                {

                    _dal.Add(user);   
                    await _dal.SaveChangesAsync();

                   
                    HttpContext.Session.SetString("StudentName", user.StudentName);
                    HttpContext.Session.SetString("StudentMail", user.StudentMail);
                    HttpContext.Session.SetString("StudentAddress", user.StudentAddress);
                    HttpContext.Session.SetString("StudentDOB", user.StudentDOB);
                    HttpContext.Session.SetString("StudentPassword", user.StudentPassword);
                    HttpContext.Session.SetString("UserRole", user.UserRole);



                    //ViewBag.username = HttpContext.Session.GetString("name");

                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));

                    var sessionUsers = JsonConvert.DeserializeObject<StudentClass>(HttpContext.Session.GetString("SessionUser"));
                    return RedirectToAction("Index");

                }
                else
                {
                    Equals("Invalid Request");
                    return Content("InValid Data");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        //[Route("Login")]
        //public async Task<IActionResult> Login(StudentClass modal)
        public IActionResult Login(LoginClass modal)
        {
            try
            {
                var user = _dal.studentTable.Where(x => x.StudentMail.Equals(modal.StudentMail) && x.StudentPassword.Equals(modal.StudentPassword)).FirstOrDefault();
                if (user != null)
                {

                    HttpContext.Session.SetInt32("StudentId", user.StudentId);
                    HttpContext.Session.SetString("StudentName", user.StudentName);
                    HttpContext.Session.SetString("StudentMail", user.StudentMail);
                    HttpContext.Session.SetString("StudentAddress", user.StudentAddress);
                    HttpContext.Session.SetString("StudentDOB", user.StudentDOB);
                    HttpContext.Session.SetString("StudentPassword", user.StudentPassword);
                    HttpContext.Session.SetString("StudentImage", user.StudentImage);
                    HttpContext.Session.SetString("UserRole", user.UserRole);

                    return RedirectToAction("Index");
                }
                else
                {
                    Equals("Invalid Request");
                    return Content("InValid Data");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        // [Route("Registration")]
        public async Task<IActionResult> UploadImage(IFormFile StudentImage)
        {

            Random r = new();
            int k = r.Next(200, 5000);


            if (StudentImage != null)
            { 

                string filename = EncryptionController.Encrypt(Path.GetFileNameWithoutExtension(StudentImage.FileName) + k, HttpContext.Session.GetString("StudentMail") + HttpContext.Session.GetString("StudentPassword"));
                string extention = Path.GetExtension(StudentImage.FileName);
                string filenamewithoutextention = Path.GetFileNameWithoutExtension(StudentImage.FileName);

                filename = filename + DateTime.Now.ToString("yymmssff") + extention;

               

                string image = EncryptionController.Encrypt(filename, HttpContext.Session.GetString("StudentMail")) + extention;
                string a = image.Replace(@"/", "");
                string c = a.Replace(@"+", "");
                string b = c.Replace(@"\", "");


                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");


                string filePath = Path.Combine(uploads, b);


                try
                {
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await StudentImage.CopyToAsync(fileStream);
                    }

                    var user = _dal.studentTable.Where(x => x.StudentMail.Equals(HttpContext.Session.GetString("StudentMail")) && x.StudentPassword.Equals(HttpContext.Session.GetString("StudentPassword"))).FirstOrDefault();
                    if (user != null)
                    {
                        user.StudentImage = b;
                        _dal.Entry(user).State = EntityState.Modified;
                        await _dal.SaveChangesAsync();

                        HttpContext.Session.SetString("StudentImage", user.StudentImage);
                    } 
                }
                catch (Exception e)
                {
                    throw e;
                }


            }

            return RedirectToAction("Index");

        }









        [HttpGet]
        public ActionResult AddingPost()
        {
            return PartialView();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        // [Route("Registration")]
        public async Task<IActionResult> AddPost(StudentPostClass model, IFormFile StudentPostImage)
        {

            Random r = new();
            int k = r.Next(200, 5000);


            if (StudentPostImage != null)
            {

                string filename = EncryptionController.Encrypt(Path.GetFileNameWithoutExtension(StudentPostImage.FileName) + k, HttpContext.Session.GetString("StudentMail") + HttpContext.Session.GetString("StudentPassword"));
                string extention = Path.GetExtension(StudentPostImage.FileName);
                string filenamewithoutextention = Path.GetFileNameWithoutExtension(StudentPostImage.FileName);

                filename = filename + DateTime.Now.ToString("yymmssff") + extention;

                

                string image = EncryptionController.Encrypt(filename, HttpContext.Session.GetString("StudentMail")) + extention;
                string a = image.Replace(@"/", "");
                string c = a.Replace(@"+", "");
                string b = c.Replace(@"\", "");


                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "userpost");


                string filePath = Path.Combine(uploads, b);


                try
                {
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await StudentPostImage.CopyToAsync(fileStream);
                    }

                    var user = _dal.studentTable.Where(x => x.StudentMail.Equals(HttpContext.Session.GetString("StudentMail")) && x.StudentPassword.Equals(HttpContext.Session.GetString("StudentPassword"))).FirstOrDefault();
                    if (user != null)
                    {
                        model.StudentPostProfileImage =user.StudentImage;
                        model.StudentPostImage = b;
                        model.StudentPostProfileName = user.StudentName;



                        _dal.Add(model);
                        
                        
                        await _dal.SaveChangesAsync();

                         
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }


            }

            return RedirectToAction("Index");

        }






        [HttpGet]
        public ActionResult ViewPost()
        {
             return View(_dal.studentPostTable.ToList());
        }


        [HttpPost]
      public ActionResult ProfileImage()
        {
            return PartialView();
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SocialMedia()
        {
            return View(_dal.studentPostTable.ToList());
        }



        public IActionResult LoginForm()
        {
            return PartialView();
        }


        
        public IActionResult RegistrationForm()
        {
            return PartialView();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
