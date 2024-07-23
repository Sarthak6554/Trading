# .NET Samples

Setup and Configurations

1. Dotnet 6 SDK must be installed to run the application

2. Provide the value to global variables in appsettings.Development.json

  "exchangeRootPath": "Path to root folder of exchange",
  "batchSize" :  "How many random values we need to pick and start predicting the next values for stocks"

  Sample Values
  "exchangeRootPath": "C:\\Users\\Sarthak_Pandit\\Downloads\\(TC1)(TC2) stock_price_data_files",
  "batchSize" :  10

  Note: Path to root folder of exchange should have following directory structure
  
  (TC1)(TC2) stock_price_data_files (root directory)
	 NYSE
	     {StockName1}.csv	
	     {StockName2}.csv
	 LSE
	     {StockName}.csv
	 NASDAQ
             {StockName}.csv


To build and run your sample:

1. Go to the sample folder and build to check for errors:

    ```console
    dotnet build
    ```

2. Run your sample:

    ```console
    dotnet run
    ```

Input - "api/StockPrediction/predictStockValues" Post API 

[
    {
        "number": 3,
        "stockExchange": 0,
        "sourceType": 0
    },
    {
        "number": 3,
        "stockExchange": 1,
        "sourceType": 0
    },
    {
        "number": 3,
        "stockExchange": 2,
        "sourceType": 0
    }
]

Request Curl: 

curl --location 'https://localhost:7173/api/StockPrediction/predictStockValues' \
--header 'Content-Type: application/json' \
--data '[
    {
        "number": 1,
        "stockExchange": 0,
        "sourceType": 0
    },
    {
        "number": 1,
        "stockExchange": 1,
        "sourceType": 0
    },
    {
        "number": 1,
        "stockExchange": 2,
        "sourceType": 0
    }
]'

Description: 

number : "Represent how many stock we need to predict in given Stock Exchange"
stockExchange : "Enum representing different Exchange"
sourceType "Enum representing different data source currently only CSV is supported"

Enums

SourceType
    {
        CSV
    }

StockExchange
    {
        LSE,
        NASDAQ,
        NYSE
    }


Output: 

In the requested Stock Exchange directory Predicted stocks value csv will be generated with suffix as "_Output.csv"

Sample

 (TC1)(TC2) stock_price_data_files (root directory)
	 NYSE
	     {StockName1}.csv	
	     {StockName2}.csv
	     {StockName1_Output}.csv	
	     {StockName2_Output}.csv	
	 LSE
	     {StockName1}.csv
	     {StockName1_Output}.csv	
	 NASDAQ
             {StockName2}.csv
	     {StockName2_Output}.csv	


Future Enhancements:

1. Support for multiple data source like excel, sql, text file etc.
2. If some client like Angular/React is using to generate the predicted values so in that case we can use SignalR to send 
real time predicted stock values to UI to display.
3. We can use microservice architecture to better scalability.
4. If some stock values were unable to predict then it should stop predicting others stock. 


