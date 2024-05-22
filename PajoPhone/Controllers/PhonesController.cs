using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PajoPhone.Data;
using PajoPhone.Models;

namespace PajoPhone.Controllers
{
    public class PhonesController : Controller
    {
        private readonly PajoPhoneContext _context;

        public PhonesController(PajoPhoneContext context)
        {
            _context = context;
        }
        // GET: Phones
        public async Task<IActionResult> Index(string searchString, decimal searchPriceMin,decimal searchPriceMax)
        {
            if (_context.Phone == null)
            {
                return Problem("Entity set 'PajoPhoneContext.Phone'  is null.");
            }
            IQueryable<Phone> phone = _context.Phone;

            if (!String.IsNullOrEmpty(searchString))
            {
                phone = phone.Where(s => s.Name!.Contains(searchString));
            }
            if (searchPriceMin != 0)
            {
                phone = phone.Where(s => s.Price >= searchPriceMin);
            }
            if (searchPriceMax != 0)
            {
                phone = phone.Where(s => s.Price! <= searchPriceMax);
            }
            return View(await phone.ToListAsync());
        }
        public async Task<IActionResult> Tiles()
		{
			return View(await _context.Phone.ToListAsync());
		}
		// GET: Phones/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // GET: Phones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Color,Description,Image,Price,Path")] Phone phone)
        {
            // todo: make sure the folder exists and create if not exists
            string path = $@"E:\programming\C#\PajoPhone\PajoPhone\wwwroot\images\{phone.Image.FileName}";

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await phone.Image.CopyToAsync(fileStream);
                phone.Path = phone.Image.FileName;
                _context.Phone.Add(phone);

            }

            if (ModelState.IsValid)
            {
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Color,Description,Price")] Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.Id))
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
            return View(phone);
        }

        // GET: Phones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phone = await _context.Phone.FindAsync(id);
            if (phone != null)
            {
                _context.Phone.Remove(phone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(int id)
        {
            return _context.Phone.Any(e => e.Id == id);
        }
    }
}
