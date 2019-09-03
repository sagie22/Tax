using NUnit.Framework;

namespace Tests
{
    public class TestTax
    {
        Tax.Models.PostalCode pc;
        Tax.Services.DataService ds ;
        Tax.Controllers.IndividualTaxController tax;
        const string flatRatePostCode = "7000";
        const string flatValuePostCode = "A100";
        const string progressivePostCode = "7441";

        [SetUp]
        public void Setup()
        {
            pc = new Tax.Models.PostalCode();
            ds = new Tax.Services.DataService();
            tax = new Tax.Controllers.IndividualTaxController(ds);
        }

        [Test]
        public void TestFlatRate0k()
        {
            var income = 0M;
            Assert.AreEqual(0,tax.Calc(income, flatRatePostCode));
        }
        [Test]
        public void TestFlatRate100k()
        {
            var income = 100000M;
            Assert.AreEqual(income / 100 * 17.5M, tax.Calc(income, flatRatePostCode));
        }

        [Test]
        public void TestFlatValueUnder200k()
        {
            var income = 100000M;
            Assert.AreEqual(income / 100 * 5M, tax.Calc(income, flatValuePostCode));
        }
        [Test]
        public void TestFlatValueOver200k()
        {
            var income = 300000M;
            Assert.AreEqual(10000M, tax.Calc(income, flatValuePostCode));
        }

        [Test]
        public void TestProgressive0k()
        {
            var income = 0M;
            var taxAmount = tax.Calc(income, progressivePostCode);
            Assert.AreEqual(0, taxAmount);
        }

        [Test]
        public void TestProgressiveUnder8350()
        {
            var income = 8000M;
            var taxAmount = tax.Calc(income, progressivePostCode);
            Assert.AreEqual(income/100*10, taxAmount);
        }

        [Test]
        public void TestProgressive30k()
        {
            var income = 8450M;
            var taxAmount = tax.Calc(income, progressivePostCode);
            var calcTax = 8350M / 100M * 10M;
            calcTax += (8450M - 8351M) / 100M * 15M;
            Assert.AreEqual(calcTax, taxAmount);
        }


    }
}