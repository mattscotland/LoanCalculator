using System;

namespace LoanCalculator.Models
{
    public class LoanPayment
    {
        public int MonthNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
