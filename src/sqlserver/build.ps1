Import-Module sqlserver
Invoke-Sqlcmd -InputFile build.sql -ServerInstance "localhost"