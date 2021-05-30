using Credit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Credit.Controllers
{
    public class ListOfLoansController : Controller
    {
        private readonly creditContext _context;

        public ListOfLoansController(creditContext context)
        {
            _context = context;
        }

        // GET: TypeOfLoans
        public async Task<IActionResult> Index()
        {
            var creditContext = _context.SubmittedApplications.Include(s => s.TypeOfLoan).Include(s => s.User);
            return View(await creditContext.ToListAsync());
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
