# faang-stock-prices

## Summary

This is a RESTful API that returns historical stock price data for the FAANG companies. The API currently contains 4 endpoints used to display different types of data of interest by reading this data from a CSV file and performing various calculations inside the API. My solution is built as a .NET Core 2.1 Web API and contains 3 projects:
1. CapGroupApi - defines all the routes that clients can interact with, invokes methods from the BusinessLogic layer, and transforms the response objects into the appropriate JSON data to be returned to the client.
2. BusinessLogic - contains all the models/data-containers, all the business logic and calculations, and file-reading operations.
3. CapGroupTests - houses all the unit tests.

## Requirements

I built this API using Visual Studio, but you can run this in VS Code as a more lightweight/free approach.
1. Clone this repository (https://github.com/michaelcheung2/faang-stock-prices.git)
2. Install .NET Core 3.1 SDK or later. (https://dotnet.microsoft.com/download)
3. Install Visual Studio Code. (https://code.visualstudio.com/Download)
5. Launch Visual Studio Code, open the Terminal in the IDE, and navigate to the project directory of where you have it saved locally (i.e. "D:\faang-stock-prices\solution\CapGroupApi")
6. To run the app, type "dotnet run --project CapGroupApi" in the Terminal.
7. The app should launch and start listening on a port on your machine.
8. To run the unit tests, type "dotnet test" in the Terminal.

## Usage

After following the above instructions, your browser should launch and then you'll be able to simply type in any available endpoint in the URL bar to call the API. Available endpoints:
1. https://localhost:5001/api/v1/faangfund/first-last-days
2. https://localhost:5001/api/v1/faangfund/company-closing-price?ticker=fb&date=2018-01-02
3. https://localhost:5001/api/v1/faangfund/average-closing-price?ticker=amzn&yearMonth=2015-07
4. https://localhost:5001/api/v1/faangfund/biggest-gains


1. To find out the first and last dates represented in the relevant data:

```bash
curl "https://localhost:44303/api/v1/faangfund/first-last-days"
{"firstDate":"1989-09-19","lastDate":"2019-11-15"}
```

2. To find out the closing price of Facebook on January 2nd, 2018:

```bash
curl "https://localhost:44303/api/v1/faangfund/company-closing-price?ticker=fb&date=2018-01-02"
{"companyTicker":"FB","date":"2018-01-02","closingPrice":181.42}
```

3. To find out the average closing price of Amazon in the month of July 2015:

```bash
curl "https://localhost:44303/api/v1/faangfund/average-closing-price?ticker=amzn&yearMonth=2015-07"
{"average":478.71}
```

4. To find out the top 10 biggest gaining days, assuming the fund holds an equal number of shares of each company (the output includes the day and the % change from the previous day's close):

```bash
curl "https://localhost:44303/api/v1/faangfund/biggest-gains"
[{"companyTicker":"NFLX","date":"2013-01-24","closingPrice":"$20.98","percentGainFromPreviousDay":"42.24%"},{"companyTicker":"NFLX","date":"2002-10-10","closingPrice":"$0.51","percentGainFromPreviousDay":"37.84%"},{"companyTicker":"AMZN","date":"2001-11-26","closingPrice":"$12.21","percentGainFromPreviousDay":"34.47%"},{"companyTicker":"AMZN","date":"2001-04-09","closingPrice":"$11.18","percentGainFromPreviousDay":"33.57%"},{"companyTicker":"AAPL","date":"1997-08-06","closingPrice":"$0.94","percentGainFromPreviousDay":"32.39%"},{"companyTicker":"AMZN","date":"2001-11-14","closingPrice":"$9.49","percentGainFromPreviousDay":"30.18%"},{"companyTicker":"FB","date":"2013-07-25","closingPrice":"$34.36","percentGainFromPreviousDay":"29.61%"},{"companyTicker":"AMZN","date":"2007-04-25","closingPrice":"$56.81","percentGainFromPreviousDay":"26.95%"},{"companyTicker":"AMZN","date":"2009-10-23","closingPrice":"$118.49","percentGainFromPreviousDay":"26.80%"},{"companyTicker":"AMZN","date":"2001-01-03","closingPrice":"$17.56","percentGainFromPreviousDay":"26.51%"}]
```
