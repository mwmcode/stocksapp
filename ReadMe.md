## Database
- [MSSQL Docker](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&tabs=cli&pivots=cs1-bash) OR (better option 👇)
- [Azure SQL Edge](https://hub.docker.com/r/microsoft/azure-sql-edge)
- [Azure Data Studio](https://azure.microsoft.com/en-us/products/data-studio)

### Migrations
- install `dotnet tool install --global dotnet-ef --version 8.*`
- init `dotnet ef migrations add init`
- run `dotnet ef database update`
