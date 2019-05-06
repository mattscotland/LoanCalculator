using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoanCalculator.Web.Models;
using LoanCalculator.Interfaces;
using LoanCalculator.Models;

namespace LoanCalculator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly Fee _feeConfig;

        public HomeController(ILoanService loanService, Fee feeConfig)
        {
            _loanService = loanService;
            _feeConfig = feeConfig;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetLoanDetails(Loan loan)
        {
            loan.LoanFee = _feeConfig;
            var summary = _loanService.GetLoanSummary(loan);
            var paymentSchedule = _loanService.GetPaymentSchedule(loan, summary);
            return PartialView("LoanDetails", Tuple.Create(summary, paymentSchedule));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
