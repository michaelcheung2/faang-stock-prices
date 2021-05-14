using System;

namespace BusinessLogic.Objects
{
    /// <summary>
    /// Object containing information about a company's ticker, the date of record, 
    /// and the stock price at market close on that particular date.
    /// </summary>
    public class ClosingStockPrice
    {
        public string CompanyTicker { get; set; }
        public DateTime Date { get; set; }
        public decimal ClosingPrice { get; set; }
        public decimal PercentGainFromPreviousDay { get; set; }
    }
}