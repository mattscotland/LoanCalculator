using System;

namespace LoanCalculator.Models
{
    public class Loan
    {
        public decimal VehiclePrice { get; set; }
        public decimal Deposit { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int FinanceMonths { get; set; }
        public Fee LoanFee { get; set; }

    }
}
