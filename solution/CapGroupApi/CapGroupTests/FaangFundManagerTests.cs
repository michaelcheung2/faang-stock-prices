using NUnit.Framework;
using BusinessLogic;

namespace Tests
{
    public class FaangFundManagerTests
    {
        private FaangFundManager _FaangFundManager;

        [OneTimeSetUp]
        public void ClassInit()
        {
            _FaangFundManager = new FaangFundManager();
        }

        [Test]
        public void FaangFundManager_CalculatePercentGain_3000And3300_Returns10()
        {
            decimal yesterdayPrice = 3000.00M;
            decimal todayPrice = 3300.00M;

            var result = _FaangFundManager.CalculatePercentGain(todayPrice, yesterdayPrice);

            Assert.AreEqual(10M, result);
        }
    }
}