using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NavGallery.Data;
using NavGallery.Models;

namespace NavGallery.Controllers
{
    public class CarsController : Controller
    {
        private readonly NavGalleryContext _context;

        public CarsController(NavGalleryContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var navGalleryContext = _context.Carros.Include(c => c.Marca);
            return View(await navGalleryContext.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carros == null)
            {
                return NotFound();
            }

            var car = await _context.Carros
                .Include(c => c.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(_context.Marca, "Id", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                if (car.FotoUpload != null && car.FotoUpload.Length > 0)
                {
                    car.FotoCar = UploadImage(car.FotoUpload);
                }
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(_context.Marca, "Id", "Name", car.MarcaId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carros == null)
            {
                return NotFound();
            }

            var car = await _context.Carros.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["MarcaId"] = new SelectList(_context.Marca, "Id", "Name", car.MarcaId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (car.FotoUpload != null && car.FotoUpload.Length > 0)
                    {
                        DeleteImage(car.FotoCar);

                        car.FotoCar = UploadImage(car.FotoUpload);
                    }
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(_context.Marca, "Id", "Name", car.MarcaId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carros == null)
            {
                return NotFound();
            }

            var car = await _context.Carros
                .Include(c => c.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carros == null)
            {
                return Problem("Entity set 'NavGalleryContext.Carros'  is null.");
            }
            var car = await _context.Carros.FindAsync(id);
            if (car != null)
            {
                DeleteImage(car.FotoCar);
                _context.Carros.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return (_context.Carros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string UploadImage(IFormFile file)
        {
            var nomeUnico = Guid.NewGuid().ToString() + "_" + file.FileName;
            var caminhoDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", nomeUnico);


            if (!Directory.Exists(Path.GetDirectoryName(caminhoDestino)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoDestino));
            }

            using (var stream = new FileStream(caminhoDestino, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/imagens/" + nomeUnico;
        }
        private void DeleteImage(string caminhoImagem)
        {
            if (!string.IsNullOrEmpty(caminhoImagem))
            {
                var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminhoImagem.TrimStart('/'));
                if (System.IO.File.Exists(caminhoCompleto))
                {
                    System.IO.File.Delete(caminhoCompleto);
                }
            }
        }
    }
}
