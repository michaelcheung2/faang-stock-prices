using BusinessLogic.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class FaangFundManager
    {
        private const int DESIRED_COUNT_BIGGEST_GAINING_DAYS = 10;

        /// <summary>
        /// Get the first and last dates represented in the relevant data.
        /// </summary>
        public List<ClosingStockPrice> GetFirstLastDays()
        {
            var fileManager = new FileManager();
            var allClosingStockPrices = fileManager.GetDataFromSource();

            var response = new List<ClosingStockPrice>();

            response.Add(allClosingStockPrices[0]); // first object
            response.Add(allClosingStockPrices[allClosingStockPrices.Count - 1]); // last object

            return response;
        }

        /// <summary>
        /// Gets the closing price of a specified company on a specified date.
        /// </summary>
        /// <param name="ticker">"fb"</param>
        /// <param name="date">"2018-01-02"</param>
        /// <returns></returns>
        public ClosingStockPrice GetCompanyClosingPriceOnDate(string ticker, string date)
        {
            var fileManager = new FileManager();
            var allClosingStockPrices = fileManager.GetDataFromSource();

            foreach (ClosingStockPrice stockEntry in allClosingStockPrices)
            {
                if (stockEntry.CompanyTicker.ToUpper() == ticker.ToUpper() && stockEntry.Date == DateTime.Parse(date))
                {
                    return stockEntry;
                }                
            }

            return null;
        }

        /// <summary>
        /// Gets the average closing price of a specified company on a specified month/year.
        /// </summary>
        /// <param name="ticker">"AMZN"</param>
        /// <param name="yearMonth">"2015-07"</param>
        /// <returns></returns>
        public decimal GetAverageClosingPriceOnDate(string ticker, string yearMonth)
        {
            var date = yearMonth.Split("-");
            decimal year = decimal.Parse(date[0]);
            decimal month = decimal.Parse(date[1]);

            var fileManager = new FileManager();
            var allClosingStockPrices = fileManager.GetDataFromSource();

            // calculate the average price (sum of prices / number of days = average price)
            decimal totalPriceForGivenMonth = 0;
            int totalCountForGivenMonth = 0;
            foreach (ClosingStockPrice stockEntry in allClosingStockPrices)
            {
                if (stockEntry.CompanyTicker.ToUpper() == ticker.ToUpper() 
                    && stockEntry.Date.Year == year
                    && stockEntry.Date.Month == month)
                {
                    totalPriceForGivenMonth += stockEntry.ClosingPrice;
                    totalCountForGivenMonth++;
                }
            }

            decimal average = totalPriceForGivenMonth / totalCountForGivenMonth;
            average = Math.Round(average, 2);

            return average;
        }

        /// <summary>
        /// Gets the top 10 biggest gaining days.
        /// </summary>
        public List<ClosingStockPrice> GetBiggestGains()
        {
            var fileManager = new FileManager();
            var allClosingStockPrices = fileManager.GetDataFromSource();

            for (int i = 1; i < allClosingStockPrices.Count; i++)
            {
                var previousStock = allClosingStockPrices[i - 1];
                var currentStock = allClosingStockPrices[i];

                // calculate percentage gain or loss
                currentStock.PercentGainFromPreviousDay = CalculatePercentGain(currentStock.ClosingPrice, previousStock.ClosingPrice);
            }

            // sort by highest percentage gained entries first, then take the top x entries
            var response = allClosingStockPrices.OrderByDescending(x => x.PercentGainFromPreviousDay).Take(DESIRED_COUNT_BIGGEST_GAINING_DAYS).ToList();

            return response;
        }

        public decimal CalculatePercentGain(decimal currentPrice, decimal previousPrice)
        {
            return Math.Round((((currentPrice - previousPrice) / previousPrice) * 100), 2);
        }
    }
}
