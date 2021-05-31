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


        //// GET: SubmittedApplications
        //public async Task<IActionResult> Payments()
        //{
        //    return View();
        //}

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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: SubmittedApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("DateOfApplicationSubmission,LoanSize,TypeOfLoanId")] 
            SubmittedApplication submittedApplication)
        {
            SubmittedApplication subApp = await _context.SubmittedApplications.FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                subApp = new SubmittedApplication {
                    DateOfApplicationSubmission = submittedApplication.DateOfApplicationSubmission,
                    LoanSize = submittedApplication.LoanSize,
                    Status = "In review",
                    UserId = Int32.Parse(Request.Cookies["UserId"]),
                    TypeOfLoanId = submittedApplication.TypeOfLoanId,
                    Payments = submittedApplication.Payments
                };
                _context.SubmittedApplications.Add(subApp);
                //_context.Add(submittedApplication);
                await _context.SaveChangesAsync();
                return View("Payments", subApp);
                //return RedirectToAction(nameof());
            }
            //ViewData["TypeOfLoanId"] = new SelectList(_context.TypeOfLoans, "Id", "TypeOfLoanRate", submittedApplication.TypeOfLoanId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", submittedApplication.UserId);
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
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,DateOfApplicationSubmission,LoanSize,Status,UserId,TypeOfLoanId")] 
            SubmittedApplication submittedApplication,
            DebtPayment debPay)
        {
            if (submittedApplication.Status == "In review")
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
            }
            else if (submittedApplication.Status == "Approved")
            {
                List<double> list = new List<double> { 10, 20, 30, 30, 39.29 };
                DateTime date = submittedApplication.DateOfApplicationSubmission;
                List<double> R_i = new List<double> { 1.015, 1.031, 1.047, 1.063, 1.079 };
                List<double> payment = new List<double>();

                double D = 100;
                payment.Add(D);

                debPay = new DebtPayment
                {
                    Date = date,
                    PaymentAmount = payment[0],
                    UserId = Int32.Parse(Request.Cookies["UserId"]),
                    Paid = "In process"
                };
                _context.DebtPayments.Add(debPay);

                if (submittedApplication.LoanSize == 100000)
                {
                    for (int i = 1; i < 5; ++i)
                    {
                        payment.Add((payment[i - 1] - list[i - 1]) * R_i[i - 1]);
                        date = date.AddMonths(1);

                        debPay = new DebtPayment
                        {
                            Date = date,
                            PaymentAmount = payment[i],
                            UserId = Int32.Parse(Request.Cookies["UserId"]),
                            Paid = "In process"
                        };
                        _context.DebtPayments.Add(debPay);
                    }
                }
                if (submittedApplication.LoanSize == 200000)
                {
                    D *= 2;
                    payment.Add(D);

                    for (int i = 1; i < 5; ++i)
                    {
                        payment.Add(((payment[i - 1] * 2) - (list[i - 1] * 2)) * R_i[i - 1]);

                        date = date.AddMonths(1);

                        debPay = new DebtPayment
                        {
                            Date = date,
                            PaymentAmount = payment[i],
                            UserId = Int32.Parse(Request.Cookies["UserId"]),
                            Paid = "In process"
                        };
                        _context.DebtPayments.Add(debPay);
                    }
                }
                if (submittedApplication.LoanSize == 300000)
                {
                    D *= 3;
                    payment.Add(D);

                    for (int i = 1; i < 5; ++i)
                    {
                        payment.Add(((payment[i - 1] * 3) - (list[i - 1] * 3)) * R_i[i - 1]);

                        date = date.AddMonths(1);

                        debPay = new DebtPayment
                        {
                            Date = date,
                            PaymentAmount = payment[i],
                            UserId = Int32.Parse(Request.Cookies["UserId"]),
                            Paid = "In process"
                        };
                        _context.DebtPayments.Add(debPay);
                    }
                }
                if (submittedApplication.LoanSize == 400000)
                {
                    D *= 4;
                    payment.Add(D);

                    for (int i = 1; i < 5; ++i)
                    {
                        payment.Add(((payment[i - 1] * 4) - (list[i - 1] * 4)) * R_i[i - 1]);

                        date = date.AddMonths(1);

                        debPay = new DebtPayment
                        {
                            Date = date,
                            PaymentAmount = payment[i],
                            UserId = Int32.Parse(Request.Cookies["UserId"]),
                            Paid = "In process"
                        };
                        _context.DebtPayments.Add(debPay);
                    }
                }
                if (submittedApplication.LoanSize == 500000)
                {
                    D *= 5;
                    payment.Add(D);

                    for (int i = 1; i < 5; ++i)
                    {
                        payment.Add(((payment[i - 1] * 5) - (list[i - 1] * 5)) * R_i[i - 1]);

                        date = date.AddMonths(1);

                        debPay = new DebtPayment
                        {
                            Date = date,
                            PaymentAmount = payment[i],
                            UserId = Int32.Parse(Request.Cookies["UserId"]),
                            Paid = "In process"
                        };
                        _context.DebtPayments.Add(debPay);
                    }
                }

                await _context.SaveChangesAsync();
            }

            _context.Update(submittedApplication);
            await _context.SaveChangesAsync();

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
