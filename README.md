# ET_apiMovies
Minimal API Movies with .NET 8 and SQL Server

Steps to Run:

1. Install .NET 8 (Download .NET 8).
2. Install SQL Server.
3. Change the connection credentials in the ConnectionStrings section of appsettings.Development.json.
4. Run the project (the migration will be performed automatically if necessary).

----

If TraktApiKey in AppSettings from appsettings.Development.json expires, generate a new API key at Trakt API Applications(https://trakt.tv/oauth/applications/new) and copy and paste the new API key into TraktApiKey.