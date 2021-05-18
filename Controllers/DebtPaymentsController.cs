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
    public class DebtPaymentsController : Controller
    {
        private readonly creditContext _context;

        public DebtPaymentsController(creditContext context)
        {
            _context = context;
        }

        // GET: DebtPayments
        public async Task<IActionResult> Index()
        {
            var creditContext = _context.DebtPayments.Include(d => d.User);
            return View(await creditContext.ToListAsync());
        }

        // GET: DebtPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debtPayment = await _context.DebtPayments
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (debtPayment == null)
            {
                return NotFound();
            }

            return View(debtPayment);
        }

        // GET: DebtPayments/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address");
            return View();
        }

        // POST: DebtPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,PaymentAmount,UserId")] DebtPayment debtPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(debtPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", debtPayment.UserId);
            return View(debtPayment);
        }

        // GET: DebtPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debtPayment = await _context.DebtPayments.FindAsync(id);
            if (debtPayment == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", debtPayment.UserId);
            return View(debtPayment);
        }

        // POST: DebtPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,PaymentAmount,UserId")] DebtPayment debtPayment)
        {
            if (id != debtPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(debtPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DebtPaymentExists(debtPayment.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", debtPayment.UserId);
            return View(debtPayment);
        }

        // GET: DebtPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debtPayment = await _context.DebtPayments
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (debtPayment == null)
            {
                return NotFound();
            }

            return View(debtPayment);
        }

        // POST: DebtPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var debtPayment = await _context.DebtPayments.FindAsync(id);
            _context.DebtPayments.Remove(debtPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DebtPaymentExists(int id)
        {
            return _context.DebtPayments.Any(e => e.Id == id);
        }
    }
}
