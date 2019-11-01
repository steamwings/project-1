using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YAB.Models;

namespace YetAnotherBank.Controllers
{
    public class InterestRatesController : Controller
    {
        private readonly P1Context _context;

        public InterestRatesController(P1Context context)
        {
            _context = context;
        }

        // GET: InterestRates
        public async Task<IActionResult> Index()
        {
            return View(await _context.InterestRates.ToListAsync());
        }

        // GET: InterestRates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interestRate = await _context.InterestRates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interestRate == null)
            {
                return NotFound();
            }

            return View(interestRate);
        }

        // GET: InterestRates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InterestRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rate,Name")] InterestRate interestRate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interestRate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(interestRate);
        }

        // GET: InterestRates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interestRate = await _context.InterestRates.FindAsync(id);
            if (interestRate == null)
            {
                return NotFound();
            }
            return View(interestRate);
        }

        // POST: InterestRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rate,Name")] InterestRate interestRate)
        {
            if (id != interestRate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interestRate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterestRateExists(interestRate.Id))
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
            return View(interestRate);
        }

        // GET: InterestRates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interestRate = await _context.InterestRates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interestRate == null)
            {
                return NotFound();
            }

            return View(interestRate);
        }

        // POST: InterestRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interestRate = await _context.InterestRates.FindAsync(id);
            _context.InterestRates.Remove(interestRate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterestRateExists(int id)
        {
            return _context.InterestRates.Any(e => e.Id == id);
        }
    }
}
