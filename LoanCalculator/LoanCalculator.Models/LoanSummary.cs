using System;

namespace LoanCalculator.Models
{
    public class LoanSummary
    {
        public Loan Loan { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public int TotalMonths { get; set; }
        public int StandardMonths {
            get { return TotalMonths - 2; }
        }
        public PaymentFrequency PaymentFrequency { get; set; }
        public decimal InitialPaymentAmount { get; set; }
        public decimal StandardMonthlyPaymentAmount { get; set; }
        public decimal FinalPaymentAmount { get; set; }

        
    }
}
