using BusinessLogic.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessLogic
{
    public class FileManager
    {
        private const string CLOSING_PRICES_CSV_FILE_PATH = @"..\..\..\closing_prices.csv";

        public List<ClosingStockPrice> GetDataFromSource()
        {
            if (!File.Exists(CLOSING_PRICES_CSV_FILE_PATH))
                throw new Exception("Unable to load file. Required file [" + CLOSING_PRICES_CSV_FILE_PATH + "] was not found.");

            var result = new List<ClosingStockPrice>();

            using (var reader = new StreamReader(CLOSING_PRICES_CSV_FILE_PATH))
            {
                string headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var values = line.Split(',');
                        if (values.Length == 3)
                        {
                            result.Add(new ClosingStockPrice {
                                CompanyTicker = values[0],
                                Date = DateTime.Parse(values[1]),
                                ClosingPrice = decimal.Parse(values[2])
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
