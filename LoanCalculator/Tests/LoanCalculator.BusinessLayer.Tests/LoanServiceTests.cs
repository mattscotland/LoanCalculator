using LoanCalculator.BusinessLayer;
using LoanCalculator.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace LoanCalculator.BusinessLayerTests
{
    [TestFixture]
    public class LoanServiceTests
    {

        #region fields
        private readonly LoanService _loanService;
        private Loan _loan;
        private LoanSummary _loanSummary;
        private LoanPaymentSchedule _loanPaymentSchedule;
        private List<LoanPayment>  _loanPayments;
        #endregion
        public LoanServiceTests()
        {
            _loanService = new LoanService();
        }

        [SetUp]
        public void Setup()
        {
            _loan = new Loan()
            {
                DeliveryDate = new System.DateTime(2019, 05, 02),
                Deposit = 500,
                FinanceMonths = 24,
                VehiclePrice = 13999,
                LoanFee = new Fee()
                {
                    ArrangementFee = 80,
                    CompletionFee = 20
                }
            };

            _loanSummary = new LoanSummary()
            {
                Loan = _loan,
                TotalLoanAmount = 13599,
                TotalMonths = 24,
                PaymentFrequency = PaymentFrequency.Monthly,
                InitialPaymentAmount = 646.62m,
                StandardMonthlyPaymentAmount = 566.62m,
                FinalPaymentAmount = 586.62m
            };

            _loanPayments = new List<LoanPayment>()
                {
                    new LoanPayment() { MonthNumber = 1, PaymentDate = new System.DateTime(2019, 06, 03), PaymentAmount = _loanSummary.InitialPaymentAmount },
                    new LoanPayment() { MonthNumber = 2, PaymentDate = new System.DateTime(2019, 07, 01), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 3, PaymentDate = new System.DateTime(2019, 08, 05), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 4, PaymentDate = new System.DateTime(2019, 09, 02), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 5, PaymentDate = new System.DateTime(2019, 10, 07), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 6, PaymentDate = new System.DateTime(2019, 11, 04), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 7, PaymentDate = new System.DateTime(2019, 12, 02), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 8, PaymentDate = new System.DateTime(2020, 01, 06), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 9, PaymentDate = new System.DateTime(2020, 02, 03), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 10, PaymentDate = new System.DateTime(2020, 03, 02), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 11, PaymentDate = new System.DateTime(2020, 04, 06), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 12, PaymentDate = new System.DateTime(2020, 05, 04), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 13, PaymentDate = new System.DateTime(2020, 06, 01), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 14, PaymentDate = new System.DateTime(2020, 07, 06), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 15, PaymentDate = new System.DateTime(2020, 08, 03), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 16, PaymentDate = new System.DateTime(2020, 09, 07), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 17, PaymentDate = new System.DateTime(2020, 10, 05), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 18, PaymentDate = new System.DateTime(2020, 11, 02), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 19, PaymentDate = new System.DateTime(2020, 12, 07), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 20, PaymentDate = new System.DateTime(2021, 01, 04), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 21, PaymentDate = new System.DateTime(2021, 02, 01), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 22, PaymentDate = new System.DateTime(2021, 03, 01), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 23, PaymentDate = new System.DateTime(2021, 04, 05), PaymentAmount = _loanSummary.StandardMonthlyPaymentAmount },
                    new LoanPayment() { MonthNumber = 24, PaymentDate = new System.DateTime(2021, 05, 03), PaymentAmount = _loanSummary.FinalPaymentAmount }
                };

            _loanPaymentSchedule = new LoanPaymentSchedule()
            {
                LoanPayments = _loanPayments
            };
        }

        [Test]
        public void CanGetLoanSummary()
        {
            //Arrange

            //Act
            var result = _loanService.GetLoanSummary(_loan);
            
            //Assert    
            Assert.NotNull(result);
            Assert.AreEqual(result.TotalLoanAmount, _loanSummary.TotalLoanAmount);
            Assert.AreEqual(result.TotalMonths, _loanSummary.TotalMonths);
            Assert.AreEqual(result.StandardMonths, _loanSummary.StandardMonths);
            Assert.AreEqual(result.StandardMonthlyPaymentAmount, _loanSummary.StandardMonthlyPaymentAmount);
            Assert.AreEqual(result.PaymentFrequency, _loanSummary.PaymentFrequency);
            Assert.AreEqual(result.InitialPaymentAmount, _loanSummary.InitialPaymentAmount);
            Assert.AreEqual(result.FinalPaymentAmount, _loanSummary.FinalPaymentAmount);
            Assert.AreEqual(result.Loan, _loanSummary.Loan);

            //also check if any mocked/substituted dependencies are called x times such as db layer etc
        }

        [Test]
        public void CanGetPaymentSchedule()
        {
            //Arrange

            //Act
            var result = _loanService.GetPaymentSchedule(_loan, _loanSummary);

            //Assert    
            Assert.NotNull(result);
            Assert.AreEqual(result.LoanPayments.Count, _loanPayments.Count);

            var i = 0;
            foreach (LoanPayment lp in result.LoanPayments)
            {
                Assert.AreEqual(lp.MonthNumber, _loanPayments[i].MonthNumber);
                Assert.AreEqual(lp.PaymentAmount, _loanPayments[i].PaymentAmount);
                Assert.AreEqual(lp.PaymentDate, _loanPayments[i].PaymentDate);
                i++;
            }

            //also check if any mocked/substituted dependencies are called x times such as db layer etc
        }
    }
}