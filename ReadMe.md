## Database

- [MSSQL Docker](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&tabs=cli&pivots=cs1-bash) OR (better option 👇)
- [Azure SQL Edge](https://hub.docker.com/r/microsoft/azure-sql-edge)
- [Azure Data Studio](https://azure.microsoft.com/en-us/products/data-studio)

### Migrations

- install `dotnet tool install --global dotnet-ef --version 8.*`
- `dotnet ef migrations add [MIGRATION_NAME]` followed by `dotnet ef database update`

```sh
dotnet ef migrations add init
dotnet ef database update
```

### Mock data

- stocks table

```sql
DECLARE @Counter INT = 1;
DECLARE @MaxRecords INT = 200;

WHILE @Counter <= @MaxRecords
BEGIN
    INSERT INTO [stocks].[dbo].[Stocks] ([Symbol], [Company], [Purchase], [LastDiv], [Industry], [MarketCap])
    VALUES (
        CONCAT('SYM', @Counter),
        CONCAT('Company ', @Counter),
        ROUND(RAND() * 1000, 2),
        ROUND(RAND() * 100, 2),
        CASE
            WHEN RAND() < 0.2 THEN 'Technology'
            WHEN RAND() < 0.4 THEN 'Healthcare'
            WHEN RAND() < 0.6 THEN 'Finance'
            WHEN RAND() < 0.8 THEN 'Consumer Goods'
            ELSE 'Utilities'
        END,
        CAST(RAND() * 1000000000 AS BIGINT)
    );

    SET @Counter = @Counter + 1;
END;
```

- comments table
1. register users
2. get user ids
3. update snippet below with user ids

```sql
DECLARE @Counter INT = 1;
DECLARE @MaxRecords INT = 1000;
DECLARE @StockIdMin INT = 1;
DECLARE @StockIdMax INT = 200;

WHILE @Counter <= @MaxRecords
BEGIN
    INSERT INTO [stocks].[dbo].[Comments] ([Title], [Content], [CreatedAt], [StockId])
    VALUES (
        CONCAT('Comment Title ', @Counter),
        CONCAT('Culpa officia dolor sint dolor dolor.', @Counter, '. It provides feedback or discussion related to a stock.'),
        DATEADD(MINUTE, -1 * (RAND() * 10000), SYSDATETIME()),
        CAST((RAND() * (@StockIdMax - @StockIdMin) + 13) AS INT)
    );

    SET @Counter = @Counter + 1;
END;
```

- portfolios table
1. register users
2. manually insert records (user id & stock id)


### Modifying db
```sh
dotnet ef migrations add Identity
dotnet ef migrations add SeedRole

dotnet ef database update
```
