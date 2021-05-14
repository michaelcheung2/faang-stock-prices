using BusinessLogic;
using BusinessLogic.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CapGroupApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FaangFundController : ControllerBase
    {
        private const string CUSTOM_DATETIME_FORMAT = "yyyy-MM-dd";

        // GET api/v1/faangfund/first-last-days
        [HttpGet]
        [Route("first-last-days")]
        public ActionResult<UiFirstLastDays> GetFirstLastDays()
        {
            // Get the ClosingStockPrice objects with the oldest and newest dates.
            var faangFundManager = new FaangFundManager();
            var response = faangFundManager.GetFirstLastDays();

            // Transform the ClosingStockPrice object into a UiFirstLastDaysResponse object before returning it to caller.
            var uiResponse = new UiFirstLastDays()
            {
                FirstDate = response[0].Date.ToString(CUSTOM_DATETIME_FORMAT),
                LastDate = response[1].Date.ToString(CUSTOM_DATETIME_FORMAT)
            };

            return uiResponse;
        }

        // GET api/v1/faangfund/company-closing-price?ticker=fb&date=2018-01-02
        [HttpGet("company-closing-price")]
        public ActionResult<UiClosingStockPrice> GetCompanyClosingPriceOnDate(string ticker, string date)
        {
            // Validate input.
            if (string.IsNullOrWhiteSpace(ticker) || string.IsNullOrWhiteSpace(date))
            {
                return StatusCode(422, "Invalid input.");
            }

            // Get the ClosingStockPrice object with the specified ticker and date.
            var faangFundManager = new FaangFundManager();
            var response = faangFundManager.GetCompanyClosingPriceOnDate(ticker, date);

            if (response == null)
            {
                return StatusCode(401, "No data was found.");
            }

            // Transform the ClosingStockPrice object into a UiClosingStockPrice object before returning it to caller.
            var uiResponse = new UiClosingStockPrice()
            {
                CompanyTicker = response.CompanyTicker,
                Date = response.Date.ToString(CUSTOM_DATETIME_FORMAT),
                ClosingPrice = $"${response.ClosingPrice}"
            };

            return uiResponse;
        }

        // GET api/v1/faangfund/average-closing-price?ticker=amzn&yearMonth=2015-07
        [HttpGet("average-closing-price")]
        public ActionResult<UiAverageClosingPrice> GetAverageClosingPrice(string ticker, string yearMonth)
        {
            // Validate input.
            if (string.IsNullOrWhiteSpace(ticker) || string.IsNullOrWhiteSpace(yearMonth))
            {
                return StatusCode(422, "Invalid input.");
            }

            // Get the average closing price with the specified ticker and year/month.
            var faangFundManager = new FaangFundManager();
            var response = faangFundManager.GetAverageClosingPriceOnDate(ticker, yearMonth);

            // Transform the decimal into a UiAverageClosingPrice object before returning it to caller.
            var uiResponse = new UiAverageClosingPrice()
            {
                Average = response
            };

            return uiResponse;
        }

        // GET api/v1/faangfund/biggest-gains
        [HttpGet("biggest-gains")]
        public ActionResult<List<UiClosingStockPrice>> GetBiggestGains()
        {
            // Get the top 10 ClosingStockPrice objects with the biggest gains.
            var faangFundManager = new FaangFundManager();
            var response = faangFundManager.GetBiggestGains();

            // Transform the List of ClosingStockPrice objects into a List of UiClosingStockPrice objects before returning it to caller.
            var uiResponse = new List<UiClosingStockPrice>();
            foreach (ClosingStockPrice stockEntry in response)
            {
                uiResponse.Add(
                    new UiClosingStockPrice()
                    {
                        CompanyTicker = stockEntry.CompanyTicker,
                        Date = stockEntry.Date.ToString(CUSTOM_DATETIME_FORMAT),
                        ClosingPrice = $"${stockEntry.ClosingPrice}",
                        PercentGainFromPreviousDay = $"{stockEntry.PercentGainFromPreviousDay}%"
                    }
                );
            }
            
            return uiResponse;
        }
    }
}
