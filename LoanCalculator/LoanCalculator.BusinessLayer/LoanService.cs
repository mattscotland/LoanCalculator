using System;
using System.Collections.Generic;
using LoanCalculator.Interfaces;
using LoanCalculator.Models;

namespace LoanCalculator.BusinessLayer
{
    public class LoanService : ILoanService
    {
        public LoanService()
        {
            //No need to inject any db dependencies here as yet
        }

        public LoanSummary GetLoanSummary(Loan loan)
        {
            var totalLoanAmount = GetTotalLoanAmount(loan);
            var financeMonths = loan.FinanceMonths;
            var standardPaymentAmount = GetStandardPaymentAmount(totalLoanAmount, financeMonths);
            var initialPaymentAmount = GetInitialPaymentAmount(standardPaymentAmount, loan.LoanFee);
            var finalPaymentAmount = GetFinalPaymentAmount(standardPaymentAmount, loan.LoanFee);

            var ls = new LoanSummary()
            {
                Loan = loan,
                TotalLoanAmount = totalLoanAmount,
                TotalMonths = financeMonths,
                PaymentFrequency = PaymentFrequency.Monthly,
                InitialPaymentAmount = initialPaymentAmount,
                StandardMonthlyPaymentAmount = standardPaymentAmount,
                FinalPaymentAmount = finalPaymentAmount
            };

            return ls;
        }

        public LoanPaymentSchedule GetPaymentSchedule(Loan loan, LoanSummary summary)
        {
            var payments = new List<LoanPayment>();
            var deliveryYear = loan.DeliveryDate.Year;
            var deliveryMonth = loan.DeliveryDate.Month;
            var nextPaymentMonth = GetNextPaymentMonth(deliveryMonth);
            var nextPaymentYear = GetNextPaymentYear(deliveryMonth, deliveryYear);

            for (int i = 1; i <= loan.FinanceMonths; i++)
            {
                var lp = new LoanPayment();
                lp.MonthNumber = i;
                if (i == 1)
                    lp.PaymentAmount = summary.InitialPaymentAmount;
                else if (i == loan.FinanceMonths)
                    lp.PaymentAmount = summary.FinalPaymentAmount;
                else
                    lp.PaymentAmount = summary.StandardMonthlyPaymentAmount;

                var nextPaymentDate = GetFirstMondayOfMonth(nextPaymentYear, nextPaymentMonth);
                lp.PaymentDate = nextPaymentDate;

                nextPaymentYear = GetNextPaymentYear(nextPaymentMonth, nextPaymentYear);
                nextPaymentMonth = GetNextPaymentMonth(nextPaymentMonth);
                payments.Add(lp);
            }

            var lps = new LoanPaymentSchedule();
            lps.LoanPayments = payments;

            return lps;
        }

        private static int GetNextPaymentMonth(int deliveryMonth)
        {
            if (deliveryMonth == 12)
                return 1;
            else
                return deliveryMonth + 1;
        }

        private static int GetNextPaymentYear(int deliveryMonth, int deliveryYear)
        {
            if (deliveryMonth == 12)
                return deliveryYear + 1;
            else
                return deliveryYear;
        }

        private static DateTime GetFirstMondayOfMonth(int paymentYear, int paymentMonth)
        {
            DateTime dt = new DateTime(paymentYear, paymentMonth, 1);
            while (dt.DayOfWeek != DayOfWeek.Monday)
            {
                dt = dt.AddDays(1);
            }

            return dt;
        }

        private decimal GetTotalLoanAmount(Loan loan)
        {
            var price = loan.VehiclePrice;
            var deposit = loan.Deposit;
            var arrFee = loan.LoanFee.ArrangementFee;
            var compFee = loan.LoanFee.CompletionFee;
            var totalLoanAmount = (price - deposit + arrFee + compFee);
            return totalLoanAmount;
        }

        private decimal GetStandardPaymentAmount(decimal totalLoanAmount, int financeMonths)
        {
            return Decimal.Round((totalLoanAmount/financeMonths), 2);
        }

        private decimal GetInitialPaymentAmount(decimal standardPaymentAmount, Fee loanFee)
        {
            return Decimal.Round((standardPaymentAmount + loanFee.ArrangementFee), 2);
        }

        private decimal GetFinalPaymentAmount(decimal standardPaymentAmount, Fee loanFee)
        {
            return Decimal.Round((standardPaymentAmount + loanFee.CompletionFee), 2);
        }
    }
}
