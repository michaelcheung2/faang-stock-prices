using System;

namespace BusinessLogic.Objects
{
    public class UiClosingStockPrice
    {
        public string CompanyTicker { get; set; }
        public string Date { get; set; }
        public string ClosingPrice { get; set; }
        public string PercentGainFromPreviousDay { get; set; }
    }
}
