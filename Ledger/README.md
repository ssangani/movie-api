This CLI tool was built using dotnet to display aggregated (vested) equity positions on given date based on data from input file

## Interface

The CLI parameters are as follows:
* ImportPath - Relative path to input file from where this program is being run from
* TargetDate - Which date should be used for calculating vests. This is optional, and if unspecified it'll assume today's date
* Precision - What's the preferred decimal precision when considering fractional shares. This is optional, and if unspecified it'll only consider whole shares

## How to run it?

You'll need dotnet 5 runtime installed to be able to run this program. Once installed, you can run commands as shown below to display the output

```
dotnet run --project .\Ledger.Cli\Ledger.Cli.csproj --ImportPath=example1.csv --TargetDate=2020-04-01
dotnet run --project .\Ledger.Cli\Ledger.Cli.csproj --ImportPath=example2.csv --TargetDate=2021-01-01
dotnet run --project .\Ledger.Cli\Ledger.Cli.csproj --ImportPath=example3.csv --TargetDate=2021-01-01 --Precision=1
```

## Data format

The input expects a CSV file with no header with each line formatted as shown below. You can refer to packaged csv files to see what sample csv looks like

```
<<TYPE>>,<<EMPLOYEE ID>>,<<EMPLOYEE NAME>>,<<AWARD ID>>,<<DATE>>,<<QUANTITY>>
```

* TYPE - Can be `VEST` or `CANCEL`. Line will be ignored otherwise
* EMPLOYEE ID - Employee ID (string)
* EMPLOYEE NAME - Full name
* AWARD ID - Award ID (string)
* DATE - Date in YYYY-MM-DD format
* QUANTITY - Number of shares. Can have integer or decimal values