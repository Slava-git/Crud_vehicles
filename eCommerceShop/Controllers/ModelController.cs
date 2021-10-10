using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerceShop.ShopDbContext;
using eCommerceShop.DTO;
using Microsoft.EntityFrameworkCore;

namespace eCommerceShop.Controllers
{
    public class ModelController : Controller
    {
        private readonly VehicleDbContext _db;
        [BindProperty]
        public MakeViewModel viewModel { get; set; }
        public ModelController(VehicleDbContext db)
        {
            _db = db;
            viewModel = new MakeViewModel
            {
                Makes = _db.Makes.ToList(),
                Model = new Models.Model()
            };
        }
        public IActionResult Index()
        {
            var model = _db.Models.Include(m => m.Make);
            return View(model);
        }

        public IActionResult Create()
        {
            return View(viewModel);
        }

        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _db.Models.Add(viewModel.Model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var viewmodel = _db.Models.Find(id);
            if (viewmodel == null)
            {
                return NotFound();
            }
            _db.Models.Remove(viewmodel);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            viewModel.Model = _db.Models.Include(m => m.Make).SingleOrDefault(m => m.Id ==id);
            if (viewModel.Model == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditPost()
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _db.Update(viewModel.Model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
