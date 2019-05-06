using LoanCalculator.Models;
using System;

namespace LoanCalculator.Interfaces
{
    public interface ILoanService : IService
    {
        //string GetLoanDetails(Loan loan);

        LoanSummary GetLoanSummary(Loan loan);
        LoanPaymentSchedule GetPaymentSchedule(Loan loan, LoanSummary summary);
    }
}
