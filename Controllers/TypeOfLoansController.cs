using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Credit.Models;

namespace Credit.Controllers
{
    public class TypeOfLoansController : Controller
    {
        private readonly creditContext _context;

        public TypeOfLoansController(creditContext context)
        {
            _context = context;
        }

        // GET: TypeOfLoans
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeOfLoans.ToListAsync());
        }

        // GET: TypeOfLoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfLoan = await _context.TypeOfLoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfLoan == null)
            {
                return NotFound();
            }

            return View(typeOfLoan);
        }

        // GET: TypeOfLoans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfLoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeOfLoanRate,Percent")] TypeOfLoan typeOfLoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfLoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfLoan);
        }

        // GET: TypeOfLoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfLoan = await _context.TypeOfLoans.FindAsync(id);
            if (typeOfLoan == null)
            {
                return NotFound();
            }
            return View(typeOfLoan);
        }

        // POST: TypeOfLoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfLoanRate,Percent")] TypeOfLoan typeOfLoan)
        {
            if (id != typeOfLoan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfLoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfLoanExists(typeOfLoan.Id))
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
            return View(typeOfLoan);
        }

        // GET: TypeOfLoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfLoan = await _context.TypeOfLoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfLoan == null)
            {
                return NotFound();
            }

            return View(typeOfLoan);
        }

        // POST: TypeOfLoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfLoan = await _context.TypeOfLoans.FindAsync(id);
            _context.TypeOfLoans.Remove(typeOfLoan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfLoanExists(int id)
        {
            return _context.TypeOfLoans.Any(e => e.Id == id);
        }
    }
}
