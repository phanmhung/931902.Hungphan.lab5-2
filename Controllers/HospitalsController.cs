using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab5.Models;

namespace lab5.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly lab5.Models.AppContext _context;

        public HospitalsController(lab5.Models.AppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Hospitals.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new Hospital());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hospital model)
        {
            if (!ModelState.IsValid) return View(model);
            await _context.Hospitals.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _context.Hospitals
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var hospital = await _context.Hospitals
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Hospital model)
        {
            var hospital = await _context.Hospitals
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(model);
            hospital.Name = model.Name;
            hospital.Address = model.Address;
            hospital.Phone = model.Phone;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await _context.Hospitals
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospital = await _context.Hospitals.SingleOrDefaultAsync(m => m.Id == id);
            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}