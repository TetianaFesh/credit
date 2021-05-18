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
    public class SubmittedApplicationsController : Controller
    {
        private readonly creditContext _context;

        public SubmittedApplicationsController(creditContext context)
        {
            _context = context;
        }

        // GET: SubmittedApplications
        public async Task<IActionResult> Index()
        {
            var creditContext = _context.SubmittedApplications.Include(s => s.TypeOfLoan).Include(s => s.User);
            return View(await creditContext.ToListAsync());
        }

        // GET: SubmittedApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submittedApplication = await _context.SubmittedApplications
                .Include(s => s.TypeOfLoan)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submittedApplication == null)
            {
                return NotFound();
            }

            return View(submittedApplication);
        }

        // GET: SubmittedApplications/Create
        public IActionResult Create()
        {
            ViewData["TypeOfLoanId"] = new SelectList(_context.TypeOfLoans, "Id", "TypeOfLoanRate");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address");
            return View();
        }

        // POST: SubmittedApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateOfApplicationSubmission,LoanSize,Status,UserId,TypeOfLoanId")] SubmittedApplication submittedApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(submittedApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeOfLoanId"] = new SelectList(_context.TypeOfLoans, "Id", "TypeOfLoanRate", submittedApplication.TypeOfLoanId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", submittedApplication.UserId);
            return View(submittedApplication);
        }

        // GET: SubmittedApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submittedApplication = await _context.SubmittedApplications.FindAsync(id);
            if (submittedApplication == null)
            {
                return NotFound();
            }
            ViewData["TypeOfLoanId"] = new SelectList(_context.TypeOfLoans, "Id", "TypeOfLoanRate", submittedApplication.TypeOfLoanId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", submittedApplication.UserId);
            return View(submittedApplication);
        }

        // POST: SubmittedApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfApplicationSubmission,LoanSize,Status,UserId,TypeOfLoanId")] SubmittedApplication submittedApplication)
        {
            if (id != submittedApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submittedApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmittedApplicationExists(submittedApplication.Id))
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
            ViewData["TypeOfLoanId"] = new SelectList(_context.TypeOfLoans, "Id", "TypeOfLoanRate", submittedApplication.TypeOfLoanId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", submittedApplication.UserId);
            return View(submittedApplication);
        }

        // GET: SubmittedApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submittedApplication = await _context.SubmittedApplications
                .Include(s => s.TypeOfLoan)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submittedApplication == null)
            {
                return NotFound();
            }

            return View(submittedApplication);
        }

        // POST: SubmittedApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submittedApplication = await _context.SubmittedApplications.FindAsync(id);
            _context.SubmittedApplications.Remove(submittedApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmittedApplicationExists(int id)
        {
            return _context.SubmittedApplications.Any(e => e.Id == id);
        }
    }
}
