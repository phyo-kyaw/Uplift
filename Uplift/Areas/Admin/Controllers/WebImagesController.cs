using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data;
//using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class WebImagesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WebImagesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            WebImages imageObj = new WebImages();
            if (id != null)
            {
                imageObj = _db.WebImages.SingleOrDefault(m => m.Id == id);//  _unitOfWork.Category.Get(id.GetValueOrDefault());
                if (imageObj == null)
                {
                    return NotFound();
                }
            }
            return View(imageObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(int id, WebImages imageObj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count > 0)
                {
                    byte[] p1 = null;
                    using(var fs1= files[0].OpenReadStream())
                    {
                        using(var ms1= new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    imageObj.Picture = p1;
                }
                if (imageObj.Id == 0)
                {
                    _db.WebImages.Add(imageObj);    //_unitOfWork.Category.Add(category);
                }
                else
                {
                    var imageFromDb = _db.WebImages.Where(c => c.Id == id).FirstOrDefault();

                    imageFromDb.Name = imageObj.Name;
                    if(files.Count > 0)
                    {
                        imageFromDb.Picture = imageObj.Picture;
                    }
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(imageObj);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            //origin
            //_db.
            return Json(new { data = _db.WebImages.ToList() });

            //return Json(new { data = _unitOfWork.SP_Call.ReturnList<Category>(SD.usp_GetAllCategory, null) });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _db.WebImages.Find(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _db.WebImages.Remove(objFromDb);
            _db.SaveChanges();
            return Json(new { success = true, message = "Delete successful." });
        }

        #endregion
    }
}
